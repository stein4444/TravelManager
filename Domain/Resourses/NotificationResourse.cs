using System;
using System.Collections.Generic;
using TravelManager.Domain.Entities;

namespace TravelManager.Domain.Resourses
{
    public class NotificationResourse
    {
        private Dictionary<NotificationType, Uri> _notificationImages;

        public Dictionary<NotificationType, Uri> NotificationImages
        {
            get
            {
                if (_notificationImages == null)
                {
                    _notificationImages = new Dictionary<NotificationType, Uri>()
                    {
                        { NotificationType.Welcome, new Uri($"pack://application:,,,/Images/welcome.png")},
                        { NotificationType.Error, new Uri($"pack://application:,,,/Images/error.png") },
                        { NotificationType.Info, new Uri($"pack://application:,,,/Images/info.png") },
                        { NotificationType.Warning, new Uri($"pack://application:,,,/Images/warning.png")}
                    };
                }
                return _notificationImages;
            }
        }
    }
}
