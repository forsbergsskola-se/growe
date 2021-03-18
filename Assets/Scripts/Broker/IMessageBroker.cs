using System;

namespace Broker {
    public interface IMessageBroker {
        void SubscribeTo<TMessage>(Action<TMessage> callBack);
        void UnSubscribeFrom<TMessage>(Action<TMessage> callBack);
        void Send<TMessage>(TMessage callback);
    }
}