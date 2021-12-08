using Ninject.Extensions.Factory;
using Ninject.Modules;
using TravelManager.ApplicationServices.Wrappers;
using TravelManager.Domain.Entities;
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
            Bind<ITripGraphicsManager>().To<TripGraphicsManager>().InSingletonScope();
            Bind<IMessageBoxWrapper>().To<MessageBoxWrapper>().InTransientScope();

            Bind<IMapViewModel>().To<MapViewModel>().InSingletonScope();
            Bind<IMenuViewModel>().To<MenuViewModel>().InTransientScope();

            Bind<IMainWindowViewModel>().To<MainWindowViewModel>().InSingletonScope();
            Bind<IViewModelFactory>().ToFactory().InSingletonScope();
            Bind<IMessageBusWrapper<Notification>>().To<MessageBusWrapper>().InSingletonScope();

            Bind<INotificationViewModel>().To<NotificationViewModel>().InTransientScope();
        }
    }
}
