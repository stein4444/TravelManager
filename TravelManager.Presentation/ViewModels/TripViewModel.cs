using System;
using TravelManager.ApplicationServices.ViewModels.Base;
using TravelManager.Domain.Entities;

namespace TravelManager.Presentation.ViewModels
{
    public class TripViewModel : ViewModelBase
    {
        private TripModel _tripModel;

        public TripViewModel(TripModel model)
        {
            _tripModel = model;
        }

        public string Name
        {
            get { return _tripModel.Name; }
            set
            {
                _tripModel.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get { return _tripModel.Description; }
            set
            {
                _tripModel.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public DateTime VisitDate
        {
            get { return _tripModel.VisitDate; }
            set
            {
                _tripModel.VisitDate = value;
                OnPropertyChanged(nameof(VisitDate));
            }
        }

        public TripType Type
        {
            get { return _tripModel.Type; }
            set
            {
                _tripModel.Type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
    }
}
