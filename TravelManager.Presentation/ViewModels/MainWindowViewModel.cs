using TravelManager.ApplicationServices.ViewModels.Base;

namespace TravelManager.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region PrivateFields
        private string _applicationTitile = "Travel Manager";
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
            set { SetProperty(ref _applicationTitile, value); }
        }

        public MainWindowViewModel()
        {
            MapViewModel = new MapViewModel();
            MenuViewModel = new MenuViewModel();
            StatusViewModel = new StatusBarViewModel();
        }
    }
}
