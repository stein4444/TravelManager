using MahApps.Metro.Controls;
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
            this.DataContext = IocKernel.Get<IMainWindowViewModel>();
        }
    }
}
