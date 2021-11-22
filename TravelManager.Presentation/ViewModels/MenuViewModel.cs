using Esri.ArcGISRuntime.Geometry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TravelManager.ApplicationServices.ViewModels.Base;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Interfaces;
using TravelManager.Presentation.Commands;

namespace TravelManager.Presentation.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private const int MIN_TRIP_NAME_LENGTH = 3;
        private const string TRIPID = "id";
        private readonly ITripGraphicsManager _tripManager;
        private TripModel _newTrip;

        public MenuViewModel(ITripGraphicsManager tripManager)
        {
            _tripManager = tripManager;
            _newTrip = new TripModel(); 
            AllTrips = new ObservableCollection<TripViewModel>();
            _cursoreType = CursorType.Arrow.ToString();
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

        private string _cursoreType;
        public string CursoreType
        {
            get { return _cursoreType; }
            set 
            { 
                _cursoreType = value;
                RemoveCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
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
                      _newTrip.Name = Name;
                      _newTrip.Description = Description;
                      _newTrip.Type = Type;
                      _newTrip.VisitDate = VisitDate;
                      AllTrips.Add(new TripViewModel(_newTrip));
                      ResetTripForm();
                      RemoveCommand.RaiseCanExecuteChanged();
                  }, obj =>
                  {
                      return Name?.Length > MIN_TRIP_NAME_LENGTH && _newTrip.Point != null;
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
                        var graphic = await _tripManager.DrawTrip(Type, Name, _newTrip.Id);
                        _newTrip.Point = (MapPoint)graphic.Geometry;
                        DrawCommand.RaiseCanExecuteChanged();
                        AddTrip.RaiseCanExecuteChanged();
                        RemoveCommand.RaiseCanExecuteChanged();
                    }, obj =>
                    {
                        return _newTrip.Point == null; 
                    }
                    ));
            }
        }

        private DelegateCommand _removeCommand;

        public DelegateCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??
                    (_removeCommand = new DelegateCommand(async obj =>
                    {
                        CursoreType = CursorType.Cross.ToString();
                        var graphic = await _tripManager.DeleteTrip();
                        var id = (string)graphic.Attributes[TRIPID];
                        RemoveDeletedTrip(id);
                        CursoreType = CursorType.Arrow.ToString();
                    }, obj =>
                    {
                        return AllTrips.Count != 0;
                    }
                    ));
            }
        }

        #region Private Methods
        private void ResetTripForm()
        {
            Name = null;
            Description = null;
            VisitDate = DateTime.Today;
            Type = TripType.Beach;

            _newTrip = new TripModel();
            DrawCommand.RaiseCanExecuteChanged();
        }

        private void RemoveDeletedTrip(string id)
        {
            var trip = AllTrips.FirstOrDefault(s => s.Id == id);
            AllTrips.Remove(trip);
        }
        #endregion
    }
}
