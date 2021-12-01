using TravelManager.ApplicationServices.ViewModels.Base;
using TravelManager.Domain.Interfaces;
using TravelManager.Presentation.DependencyInjection;
using TravelManager.Presentation.ViewModels.Factories;
using TravelManager.Presentation.ViewModelsInterfaces;

namespace TravelManager.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        #region PrivateFields
        private string _applicationTitile = "Travel Manager";
        #endregion

        #region ViewModels
        public IMapViewModel MapViewModel { get; set; }
        public IMenuViewModel MenuViewModel { get; set; }
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

        public MainWindowViewModel(IViewModelFactory viewModelFactory)
        {
            MapViewModel = viewModelFactory.CreateMapViewModel();
            MenuViewModel = viewModelFactory.CreateMenuViewModel();
        }
    }
}
