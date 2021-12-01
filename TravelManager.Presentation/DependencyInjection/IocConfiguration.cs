using Esri.ArcGISRuntime.UI.Controls;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using TravelManager.Domain.Interfaces;
using TravelManager.Presentation.GraphicsManger;
using TravelManager.Presentation.ViewModels;
using TravelManager.Presentation.ViewModels.Factories;
using TravelManager.Presentation.ViewModelsInterfaces;

namespace TravelManager.Presentation.DependencyInjection
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<ITripGraphicsManager>().To<TripGraphicsManager>().InTransientScope();
            Bind<IMessageBoxWrapper>().To<MessageBoxWrapper>().InTransientScope();

            Bind<IMapViewModel>().To<MapViewModel>().InTransientScope();
            Bind<IMenuViewModel>().To<MenuViewModel>().InTransientScope();

            Bind<IMainWindowViewModel>().To<MainWindowViewModel>().InSingletonScope();
            Bind<IViewModelFactory>().ToFactory().InSingletonScope();
        }
    }
}
