using Esri.ArcGISRuntime.UI;
using MahApps.Metro.Controls;
using TravelManager.Presentation.GraphicsManger;
using TravelManager.Presentation.ViewModels;

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

            var tripsOverlay = new GraphicsOverlay();

            MainMap.GraphicsOverlays.Add(tripsOverlay);

            TripGraphicsManager tripManager = new TripGraphicsManager(MainMap);
            MessageBoxWrapper messageBoxWrapper = new MessageBoxWrapper();

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(tripManager,messageBoxWrapper);
            this.DataContext = mainWindowViewModel;
        }
    }
}
