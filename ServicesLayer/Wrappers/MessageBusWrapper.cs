using ReactiveUI;
using System;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Interfaces;

namespace TravelManager.ApplicationServices.Wrappers
{
    public class MessageBusWrapper : IMessageBusWrapper<Notification>
    {
        public void Listen(string contact, Action<Notification> action)
        {
            MessageBus.Current.Listen<Notification>(contact).Subscribe(action);
        }

        public void SendMessage(Notification notification, string contact)
        {
            MessageBus.Current.SendMessage(notification, contact);
        }
    }
}
