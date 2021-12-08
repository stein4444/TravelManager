
using System;

namespace TravelManager.Domain.Interfaces
{
    public interface IMessageBusWrapper<T>
    {
        void Listen(string contact, Action<T> action);
        void SendMessage(T notification, string contact);
    }
}
