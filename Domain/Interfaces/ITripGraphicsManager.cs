﻿using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.UI;
using System.Threading.Tasks;
using TravelManager.Domain.Entities;

namespace TravelManager.Domain.Interfaces
{
    public interface ITripGraphicsManager
    {
        void DeleteTripGraphicById(string id);
        Task<Graphic> DeleteTrip();
        Task<Graphic> DrawTrip(TripType type, string label, string id);
    }
}
