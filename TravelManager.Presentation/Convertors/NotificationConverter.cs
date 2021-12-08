using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Resourses;

namespace TravelManager.Presentation.Convertors
{
    public class NotificationConverter : IValueConverter
    {
        private static NotificationResourse _notificationResourse = new NotificationResourse();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }

            Uri imagePath = _notificationResourse.NotificationImages[(NotificationType)value];
            return imagePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
