﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Resourses;

namespace TravelManager.Presentation.Convertors
{
    public class TripTypeToUriConverter : IValueConverter
    {
        private static ImageResourse _imageDictionary = new ImageResourse();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }
               
            Uri imagePath = _imageDictionary.Images[(TripType)value];
            return imagePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}