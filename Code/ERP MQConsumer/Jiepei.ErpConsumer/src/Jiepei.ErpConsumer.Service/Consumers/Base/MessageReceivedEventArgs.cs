using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jiepei.ErpConsumer.Service.Consumers.Base
{
    public class MessageReceivedEventArgs<T> : EventArgs where T : class
    {
        public T Data { get; set; }

        public BasicDeliverEventArgs OriginalArgs { get; set; }

        public MessageReceivedEventArgs(T data, BasicDeliverEventArgs originalArgs)
        {
            Data = data;
            OriginalArgs = originalArgs;
        }
    }
}
