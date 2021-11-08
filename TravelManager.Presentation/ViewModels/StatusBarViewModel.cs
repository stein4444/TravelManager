using TravelManager.ApplicationServices.ViewModels.Base;

namespace TravelManager.Presentation.ViewModels
{
    class StatusBarViewModel : ViewModelBase
    {
        private string _status = "Status";

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public string Status
        {
            get => _status;
            set { SetProperty(ref _status, value); }
        }
    }
}
