using TravelManager.ApplicationServices.ViewModels.Base;

namespace TravelManager.Presentation.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        #region ViewModels
        public MapViewModel MapVm { get; set; }
        public MenuViewModel MenuVm { get; set; }
        public StatusBarViewModel StatusVm { get; set; }
        #endregion

        #region PrivateFields
        private string _applicationTitile = "Travel Manager";
        #endregion

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public string Title
        {
            get => _applicationTitile;
            set { SetProperty(ref _applicationTitile, value); }
        }

        public MainWindowViewModel()
        {
            MapVm = new MapViewModel();
            MenuVm = new MenuViewModel();
            StatusVm = new StatusBarViewModel();
        }
    }
}
