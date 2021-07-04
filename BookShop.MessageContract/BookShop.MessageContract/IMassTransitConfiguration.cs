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
        
        #warning была уже у кого-то похожая реализация. Лучше сюда не выносить очереди, совсем не факт что приложение будет работать только с одной 
        #warning очередь на отправку и на приём сообщений. лучше выносить название очередей в константы внутри приложения или в нугет пакеты. 
        public string InputQueue { get; }
        public string OutputQueue { get; }

        public Uri QueueAddress => new Uri(RabbitMqAddress + "/" + OutputQueue);
    }
}