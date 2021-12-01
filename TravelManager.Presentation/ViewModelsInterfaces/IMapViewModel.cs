using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;

namespace TravelManager.Presentation.ViewModelsInterfaces
{
    public interface IMapViewModel
    {
        Map Map { get; set; }
        GraphicsOverlay TripsOverlay { get; set; }
    }
}
