using System;

namespace TravelManager.Domain.Entities
{
    public class TripModel 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime VisitDate { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public TripType Type { get; set; }
    }
}
