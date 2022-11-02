using Domain.Entity;
using Domain.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.RabbitMQ.Service
{
    public class MessageService : IMessageService
    {
        private readonly RabbitMQSettings settings;
        private readonly IModel channel;

        public MessageService(RabbitMQSettings settings, IModel channel)
        {
            this.settings = settings;
            this.channel = channel;
        }

        public Task PostMessage(MessageEntity message)
        {
            var messageSerialized = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageSerialized);

            channel.BasicPublish(exchange: settings.ExchangeName,
                                 routingKey: $"{message.MyNick}-{message.FriendNick}.message-sent",
                                 basicProperties: null,
                                 body: body);

            return Task.CompletedTask;
        }
    }
}
