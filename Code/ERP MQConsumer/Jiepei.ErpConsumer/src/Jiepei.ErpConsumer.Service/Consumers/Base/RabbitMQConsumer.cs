using Jiepei.ERP.Shared.Consumers.Orders;
using Jiepei.ErpConsumer.Business;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.ErpConsumer.Service.Consumers.Base
{
    public class RabbitMQConsumer: ITransientInject
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQConsumer(ILogger<RabbitMQConsumer> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public virtual ushort PrefetchCount { get; set; } = 1;

        public void Start<T>(string exchangeName, string queueName, string routeName, AsyncEventHandler<MessageReceivedEventArgs<T>> eventHandler, int retrySeconds = 10, int maxRetryCount = 10) where T : MQ_BaseOrderDto, new()
        {
            var factory = GetConnectionFactory();
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routeName);

            _channel.BasicQos(0, prefetchCount: PrefetchCount, false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var objMessage = JsonConvert.DeserializeObject<T>(message);
                var args = new MessageReceivedEventArgs<T>(objMessage, ea);
                try
                {
                    await eventHandler.Invoke(sender, args);
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _channel.BasicAck(ea.DeliveryTag, false);
                    if (objMessage.RetryCount < maxRetryCount)
                    {
                        _logger.LogError(ex, "{0}消费异常", queueName);
                        objMessage.RetryCount++;
                        string retryMessage = JsonConvert.SerializeObject(objMessage);
                        await RetryAsync(exchangeName, queueName, routeName, retryMessage, retrySeconds);
                    }
                }
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        public void Stop()
        {
            _channel?.Dispose();
            _connection?.Close();
        }

        #region private
        private ConnectionFactory GetConnectionFactory()
        {
            return ConnectionFactoryBuilder.Build(_configuration.GetSection("RabbitMQConnection:rabbitmq").Value);
        }

        private async Task RetryAsync(string exchangeName, string queueName, string routeName, string message, int retrySeconds = 10)
        {
            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(retrySeconds * 1000);
                _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routeName);
                var body = Encoding.UTF8.GetBytes(message);
                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;
                _channel.BasicPublish(exchange: exchangeName,
                                     routingKey: routeName,
                                     basicProperties: properties,
                                     body: body);
            });
        }
        #endregion

    }
}
