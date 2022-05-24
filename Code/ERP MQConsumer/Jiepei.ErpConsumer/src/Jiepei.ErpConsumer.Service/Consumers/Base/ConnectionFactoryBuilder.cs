using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jiepei.ErpConsumer.Service.Consumers.Base
{
    public class ConnectionFactoryBuilder
    {
        public static ConnectionFactory Build(string connectionString)
        {
            var factory = new ConnectionFactory();
            var arr = connectionString.Split(';').ToList();
            foreach (var config in arr)
            {
                string[] itemArray = config.Split('=');
                switch (itemArray[0].ToLower().Trim())
                {

                    case "vhost":
                        factory.VirtualHost = itemArray[1].Trim();
                        break;
                    case "host":
                        string[] hostArr = itemArray[1].Split(':');
                        factory.HostName = hostArr[0].Trim();
                        factory.Port = Convert.ToInt32(hostArr[1]);
                        break;
                    case "username":
                        factory.UserName = itemArray[1].Trim();
                        break;
                    case "password":
                        factory.Password = itemArray[1].Trim();
                        break;
                    default:
                        break;
                }
            }
            return factory;
        }
    }
}
