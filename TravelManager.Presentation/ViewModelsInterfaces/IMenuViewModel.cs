using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TravelManager.Domain.Entities;
using TravelManager.Presentation.Commands;
using TravelManager.Presentation.ViewModels;

namespace TravelManager.Presentation.ViewModelsInterfaces
{
    public interface IMenuViewModel
    {
        ObservableCollection<TripViewModel> AllTrips { get; set; }
        IEnumerable<TripType> TripTypes { get; }
        Cursor CursorType { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        DateTime VisitDate { get; set; }
        TripType Type { get; set; }
        DelegateCommand AddTrip { get; }
        DelegateCommand DrawCommand { get; }
        DelegateCommand RemoveTripFromMap { get; }
    }
}
