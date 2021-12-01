using TravelManager.ApplicationServices.ViewModels.Base;
using TravelManager.Presentation.ViewModelsInterfaces;

namespace TravelManager.Presentation.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        IMapViewModel CreateMapViewModel();
        IMenuViewModel CreateMenuViewModel();
    }
}
