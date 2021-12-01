using Esri.ArcGISRuntime.UI;
using System.Threading.Tasks;
using TravelManager.Domain.Entities;
using Esri.ArcGISRuntime.UI.Controls;

namespace TravelManager.Domain.Interfaces
{
    public interface ITripGraphicsManager
    {
        void DeleteTripGraphicById(string id);
        Task<Graphic> DeleteTrip();
        Task<Graphic> DrawTrip(TripType type, string label, string id);
    }
}
