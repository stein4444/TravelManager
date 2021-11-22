using Esri.ArcGISRuntime.Geometry;
using System;

namespace TravelManager.Domain.Entities
{
    public class TripModel 
    {
        public TripModel()
        {
            Id = Guid.NewGuid().ToString();
            VisitDate = DateTime.Today;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime VisitDate { get; set; }
        public MapPoint Point { get; set; }
        public TripType Type { get; set; }
    }
}
