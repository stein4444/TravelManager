using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using MahApps.Metro.Controls;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using TravelManager.Domain.Entities;
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
        private GraphicsOverlay _sketchOverlay;
        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

            // Creates overlay for the MapView
            _sketchOverlay = new GraphicsOverlay();

            // Add created overlay to the MapView
            MainMap.GraphicsOverlays.Add(_sketchOverlay);

            mainWindowViewModel.MapViewModel.TripsOverlay = _sketchOverlay;
        }
        
        private async void DrawButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedPoi = (Poi)PoiTypesListBox.SelectedItem;
                var compositeSymbol = GenerateCompositeSymbolWithTextLabelFromResources(selectedPoi.ImagePath);

                await CreatePictureMarkerSymbolFromResources(compositeSymbol);
            }
            catch (TaskCanceledException)
            {
                // Ignore ... let the user cancel drawing
            }
            catch (Exception ex)
            {
                // Report exceptions
                MessageBox.Show("Error drawing graphic shape: " + ex.Message);
            }
        }

        private async Task CreatePictureMarkerSymbolFromResources(CompositeSymbol compositeSymbol)
        {
            // Wait for the user to click a location on the map
            Geometry mapPoint = await MainMap.SketchEditor.StartAsync(SketchCreationMode.Point, false);

            if(mapPoint != null)
            {
                // Create graphic with the location and symbol
                Graphic pinGraphic = new Graphic((MapPoint)mapPoint, compositeSymbol);

                // Add graphic to the graphics overlay
                _sketchOverlay.Graphics.Add(pinGraphic);
            } 
        }

        private CompositeSymbol GenerateCompositeSymbolWithTextLabelFromResources(string imagePath)
        {
            // Get image from local resource by path from UI 
            Uri fileUri = new Uri($"pack://application:,,,{imagePath}");

            // Creates marker symbol from uri
            PictureMarkerSymbol pinSymbol = new PictureMarkerSymbol(fileUri)
            {
                OffsetY = 20,
                Width = 40,
                Height = 40
            };

            var textSymbol = new TextSymbol(
                PositionName.Text, Color.Black, 15, ArcGISHorizontalAlignment.Center, ArcGISVerticalAlignment.Top);
            textSymbol.BackgroundColor = Color.DarkGreen;

            var compositeSymbol = new CompositeSymbol();
            compositeSymbol.Symbols.Add(pinSymbol);
            compositeSymbol.Symbols.Add(textSymbol);

            return compositeSymbol;
        }
    }
}
