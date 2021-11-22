using Esri.ArcGISRuntime.UI;
using System.Threading.Tasks;
using TravelManager.Domain.Entities;

namespace TravelManager.Domain.Interfaces
{
    public interface ITripGraphicsManager
    {
        Task<Graphic> DrawTrip(TripType type, string label, string id);
        Task<Graphic> DeleteTrip();
    }
}
