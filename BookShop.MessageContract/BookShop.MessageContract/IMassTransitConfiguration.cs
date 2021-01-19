using System;

namespace BookShop.MessageContract
{
    public interface IMassTransitConfiguration
    {
        public string RabbitMqAddress { get; }
        public string UserName { get; }
        public string Password { get; }
        public bool Durable { get; }
        public bool PurgeOnStartup { get; }
        public string InputQueue { get; }
        public string OutputQueue { get; }

        public Uri QueueAddress => new Uri(RabbitMqAddress + "/" + OutputQueue);
    }
}