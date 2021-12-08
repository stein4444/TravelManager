using MahApps.Metro.Controls;
using TravelManager.Domain.Interfaces;
using TravelManager.Presentation.DependencyInjection;
using TravelManager.Presentation.ViewModelsInterfaces;

namespace TravelManager.Presentation.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            IocKernel.Get<ITripGraphicsManager>().Map = MainMap;
            this.DataContext = IocKernel.Get<IMainWindowViewModel>();
        }
    }
}
