using TravelManager.ApplicationServices.ViewModels.Base;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Interfaces;
using TravelManager.Domain.Resourses;
using TravelManager.Presentation.ViewModelsInterfaces;

namespace TravelManager.Presentation.ViewModels
{
    public class NotificationViewModel : ViewModelBase, INotificationViewModel
    {
        private static NotificationDictionary _notificationDictionary = new NotificationDictionary();
        private readonly IMessageBusWrapper<Notification> _messageBusWrapper;
        private Notification _notification;
        public NotificationViewModel(Notification notification, IMessageBusWrapper<Notification> messageBusWrapper)
        {
            _notification = notification;
            _messageBusWrapper = messageBusWrapper;
            _messageBusWrapper.Listen("Contract1", x => Notify(x));
        }

        private NotificationType _notificationType;
        public NotificationType NotificationType
        {
            get { return _notificationType; }
            set
            {
                _notificationType = value;
                OnPropertyChanged();
            }
        }
        public string Message
        {
            get { return _notification.Message; }
            set 
            {
                _notification.Message = value;
                OnPropertyChanged();
            }
        }

        public void Notify(Notification notification)
        {
            switch (notification.Type)
            {
                case SpecificNotifications.DrawError:
                    {
                        Message = _notificationDictionary.NotificationCollection[notification.Type];
                        NotificationType = NotificationType.Error;
                        break;
                    }
                case SpecificNotifications.ExportError:
                    {
                        Message = _notificationDictionary.NotificationCollection[notification.Type];
                        NotificationType = NotificationType.Error;
                        break;
                    }
                case SpecificNotifications.SaveError:
                    {
                        Message = _notificationDictionary.NotificationCollection[notification.Type];
                        NotificationType = NotificationType.Error;
                        break;
                    }
                case SpecificNotifications.SaveInfo:
                    {
                        Message = _notificationDictionary.NotificationCollection[notification.Type];
                        NotificationType = NotificationType.Info;
                        break;
                    }
                case SpecificNotifications.DrawInfo:
                    {
                        Message = _notificationDictionary.NotificationCollection[notification.Type];
                        NotificationType = NotificationType.Info;
                        break;
                    }
                case SpecificNotifications.ExportInfo:
                    {
                        Message = _notificationDictionary.NotificationCollection[notification.Type];
                        NotificationType = NotificationType.Info;
                        break;
                    }
                case SpecificNotifications.SaveWarning:
                    {
                        Message = _notificationDictionary.NotificationCollection[notification.Type];
                        NotificationType = NotificationType.Warning;
                        break;
                    }
                case SpecificNotifications.ExportWarning:
                    {
                        Message = _notificationDictionary.NotificationCollection[notification.Type];
                        NotificationType = NotificationType.Warning;
                        break;
                    }
            }
        }
    }
}
