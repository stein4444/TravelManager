using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TravelManager.ApplicationServices.ViewModels.Base;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Interfaces;
using TravelManager.Presentation.Commands;

namespace TravelManager.Presentation.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private const int MIN_TRIP_NAME_LENGTH = 3;
        private readonly ITripGraphicsManager _tripManager;

        public MenuViewModel(ITripGraphicsManager tripManager)
        {
            _tripManager = tripManager;
            AllTrips = new ObservableCollection<TripViewModel>();
        }
        public ObservableCollection<TripViewModel> AllTrips { get; set; }

        public IEnumerable<TripType> TripTypes
        {
            get
            {
                return Enum.GetValues(typeof(TripType))
                    .Cast<TripType>();
            }
        }

        #region AddNewTripProperties

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                AddTrip.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        private string _dDescription;
        public string Description
        {
            get { return _dDescription; }
            set
            {
                _dDescription = value;
                OnPropertyChanged();
            }
        }

        private DateTime _visitDate = DateTime.Today;
        public DateTime VisitDate
        {
            get
            {
                return _visitDate;
            }
            set
            {
                _visitDate = value;
                OnPropertyChanged();
            }
        }

        private TripType _type;
        public TripType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private DelegateCommand _addTrip;
        public DelegateCommand AddTrip
        {
            get
            {
                return _addTrip ??
                  (_addTrip = new DelegateCommand(obj =>
                  {
                      TripModel model = new TripModel() { Name = Name, Description = Description, Type = Type, VisitDate = VisitDate };
                      AllTrips.Add(new TripViewModel(model));
                      ResetTripForm();
                  }, obj =>
                  {
                      return Name?.Length > MIN_TRIP_NAME_LENGTH;
                  }
                  ));
            }
        }

        private DelegateCommand _drawCommand;

        public DelegateCommand DrawCommand
        {
            get 
            {
                return _drawCommand ??
                    (_drawCommand = new DelegateCommand(async obj =>
                    {
                        var graphic = await _tripManager.DrawTrip(Type, Name, "sada000");
                    }
                    ));
            }
        }

        private void ResetTripForm()
        {
            Name = null;
            Description = null;
            VisitDate = DateTime.Today;
            Type = TripType.Beach;
        }
    }
}
