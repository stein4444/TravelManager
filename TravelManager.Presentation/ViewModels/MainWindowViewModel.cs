using TravelManager.ApplicationServices.ViewModels.Base;
using TravelManager.Domain.Interfaces;

namespace TravelManager.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region PrivateFields
        private string _applicationTitile = "Travel Manager";
        private readonly ITripGraphicsManager _tripManager;

        #endregion

        #region ViewModels
        public MapViewModel MapViewModel { get; set; }
        public MenuViewModel MenuViewModel { get; set; }
        public StatusBarViewModel StatusViewModel { get; set; }
        #endregion

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title
        {
            get => _applicationTitile;
            set
            {
                _applicationTitile = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel(ITripGraphicsManager tripManager)
        {
            _tripManager = tripManager;
            MapViewModel = new MapViewModel();
            MenuViewModel = new MenuViewModel(_tripManager);
            StatusViewModel = new StatusBarViewModel();
        }
    }
}
