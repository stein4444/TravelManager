using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using TravelManager.ApplicationServices.ViewModels.Base;

namespace TravelManager.Presentation.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        private Map _map = new Map(Basemap.CreateStreetsVector());

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Map Map
        {
            get => _map;
            set
            {
                _map = value;
                OnPropertyChanged("_map");
            }
        }

        private GraphicsOverlay _tripsOverlay;
        public GraphicsOverlay TripsOverlay
        {
            get { return _tripsOverlay; }
            set { _tripsOverlay = value; }
        }
    }
}
