using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using MahApps.Metro.Controls;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Interfaces;
using TravelManager.Infrastructure.GraphicManagers;
using TravelManager.Presentation.ViewModels;
using ArcGISHorizontalAlignment = Esri.ArcGISRuntime.Symbology.HorizontalAlignment;
using ArcGISVerticalAlignment = Esri.ArcGISRuntime.Symbology.VerticalAlignment;

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

            TripGraphicsManager tripManager = new TripGraphicsManager(tripsOverlay, MainMap.SketchEditor);


            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(tripManager);
            this.DataContext = mainWindowViewModel;

        }
    }
}
