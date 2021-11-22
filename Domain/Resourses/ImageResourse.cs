using System;
using System.Collections.Generic;
using TravelManager.Domain.Entities;

namespace TravelManager.Domain.Resourses
{
    public class ImageResourse
    {
        private Dictionary<TripType, Uri> _images;

        public Dictionary<TripType, Uri> Images
        {
            get
            {
                if (_images == null)
                {
                    _images = new Dictionary<TripType, Uri>()
                    {
                        { TripType.Architecture, new Uri($"pack://application:,,,/Images/statue.jpg") },
                        { TripType.Beach, new Uri($"pack://application:,,,/Images/beach.png") },
                        { TripType.Park, new Uri($"pack://application:,,,/Images/mountains.jpg") }
                    };
                }
                return _images;
            }
        }
    }
}
