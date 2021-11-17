using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TravelManager.ApplicationServices.ViewModels.Base;
using TravelManager.Domain.Entities;
using TravelManager.Presentation.Commands;

namespace TravelManager.Presentation.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public MenuViewModel()
        {
            _newTrip = new TripViewModel(new TripModel());
            AllTrips = new ObservableCollection<TripViewModel>();
        }

        private GraphicsOverlay _tripsOverlay;

        public GraphicsOverlay TripsOverlay
        {
            get { return _tripsOverlay; }
            set { _tripsOverlay = value; }
        }

        public ObservableCollection<TripViewModel> AllTrips { get; set; }

        private TripViewModel _newTrip;
        public TripViewModel NewTrip
        {
            get { return _newTrip; }
            set
            {
                _newTrip = value;
                OnPropertyChanged("_newTrip");
            }
        }

        public IEnumerable<TripType> TripTypes
        {
            get
            {
                return Enum.GetValues(typeof(TripType))
                    .Cast<TripType>();
            }
        }

        private DelegateCommand _addTrip;
        public DelegateCommand AddTrip
        {
            get
            {
                return _addTrip ??
                  (_addTrip = new DelegateCommand(obj =>
                  {
                      AllTrips.Add(NewTrip);
                      NewTrip = new TripViewModel(new TripModel());
                  }));
            }
        }
    }
}
