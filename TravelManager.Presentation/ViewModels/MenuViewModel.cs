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
        private const string TRIP_ID = "id";
        private readonly ITripGraphicsManager _tripManager;
        private readonly IMessageBoxWrapper _messageBox;
        private TripModel _newTrip;

        public MenuViewModel(ITripGraphicsManager tripManager, IMessageBoxWrapper messageBox)
        {
            _tripManager = tripManager;
            _messageBox = messageBox;
            _newTrip = new TripModel(); 
            AllTrips = new ObservableCollection<TripViewModel>();
            _cursorType = Cursors.Arrow;
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

        private Cursor _cursorType;
        public Cursor CursorType
        {
            get { return _cursorType; }
            set 
            {
                _cursorType = value;
                RemoveTripFromMap.RaiseCanExecuteChanged();
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
                RemoveTripFromMap.RaiseCanExecuteChanged();
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

                        if (graphic == null)
                        {
                            return;
                        }

                        _newTrip.Point = (MapPoint)graphic.Geometry;

                        DrawCommand.RaiseCanExecuteChanged();
                        AddTrip.RaiseCanExecuteChanged();
                        RemoveTripFromMap.RaiseCanExecuteChanged();
                    }, obj =>
                    {
                        return _newTrip.Point == null; 
                    }
                    ));
            }
        }

        private DelegateCommand _removeTripFromMap;

        public DelegateCommand RemoveTripFromMap
        {
            get
            {
                return _removeTripFromMap ??
                    (_removeTripFromMap = new DelegateCommand(async obj =>
                    {
                        CursorType = Cursors.Cross;
                        var graphic = await _tripManager.DeleteTrip();

                        if (graphic == null)
                        {
                            return;
                        }

                        var id = (string)graphic.Attributes[TRIP_ID];
                        RemoveTripById(id);
                        CursorType = Cursors.Arrow;
                    }, obj =>
                    {
                        return AllTrips.Count != 0;
                    }
                    ));
            }
        }

        private DelegateCommand _removeTrip;

        public DelegateCommand RemoveTrip
        {
            get
            {
                return _removeTrip ??
                    (_removeTrip = new DelegateCommand(currentTrip =>
                    {
                        var result = _messageBox.Show("Do you want to delete this trip?", "Deleting dialog", DialogType.YesNo);

                        if(result == DialogResult.No)
                        {
                            return;
                        }

                        _tripManager.DeleteTripGraphicById(((TripViewModel)currentTrip).Id);
                        RemoveTripById(((TripViewModel)currentTrip).Id);
                        RemoveTripFromMap.RaiseCanExecuteChanged();
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
            RemoveTrip.RaiseCanExecuteChanged();
            RemoveTripFromMap.RaiseCanExecuteChanged();
            DrawCommand.RaiseCanExecuteChanged();
            AddTrip.RaiseCanExecuteChanged();
        }

        private void RemoveTripById(string id)
        {
            var trip = AllTrips.FirstOrDefault(s => s.Id == id);
            AllTrips.Remove(trip);
        }
        #endregion
    }
}
