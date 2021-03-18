using System;
using System.Collections.Generic;

namespace Broker {
    public class MessageBroker : IMessageBroker {
        static MessageBroker _instance;
        readonly Dictionary<Type, object> _subscribers = new Dictionary<Type, object>();
    
        public static MessageBroker Instance() {
            return _instance != null ? _instance : _instance = new MessageBroker();
        }
    
        public void SubscribeTo<TMessage>(Action<TMessage> callBack) {
            if (_subscribers.TryGetValue(typeof(TMessage), out var oldSubscribers)) {
                callBack = (oldSubscribers as Action<TMessage>) + callBack;
            }
            _subscribers[typeof(TMessage)] = callBack;
        }

        public void UnSubscribeFrom<TMessage>(Action<TMessage> callBack) {
            if (_subscribers.TryGetValue(typeof(TMessage), out var oldSubscribers)) {
                callBack = (oldSubscribers as Action<TMessage>) - callBack;

                if (callBack != null)
                    _subscribers[typeof(TMessage)] = callBack;
                else {
                    _subscribers.Remove(typeof(TMessage));
                }
            }
        }

        public void Send<T>(T callback) {
            if (_subscribers.TryGetValue(typeof(T), out var currentSubscribers)) {
                (currentSubscribers as Action<T>)?.Invoke(callback);
            }
        }
    }
}