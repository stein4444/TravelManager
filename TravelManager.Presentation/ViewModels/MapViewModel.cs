using Esri.ArcGISRuntime.Mapping;
using TravelManager.ApplicationServices.ViewModels.Base;

namespace TravelManager.Presentation.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        //public MapViewModel() { }

        private Map _map = new Map(Basemap.CreateStreetsVector());

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Map Map
        {
            get => _map;
            set { SetProperty(ref _map, value); }
        }
    }
}
