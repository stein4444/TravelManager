using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using MahApps.Metro.Controls;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using TravelManager.Domain.Entities;

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

            // Creates overlay for the MapView
            _sketchOverlay = new GraphicsOverlay();

            // Add created overlay to the MapView
            MyMap.GraphicsOverlays.Add(_sketchOverlay);
        }

        private async void DrawButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var pathToIcon = PoiTypesListBox.SelectedItem as Poi;
                if (pathToIcon.ImagePath == null)
                    MessageBox.Show($"{pathToIcon}");

                await CreatePictureMarkerSymbolFromResources(pathToIcon.ImagePath, pathToIcon.Name);
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

        private async Task CreatePictureMarkerSymbolFromResources(string path, string name)
        {
            // Wait for the user to click a location on the map
            Geometry mapPoint = await MyMap.SketchEditor.StartAsync(SketchCreationMode.Point, false);

            var compositeSymbol = GenerateCompositeSymbolWithTextLabelFromResources(path, name);

            // Create graphic with the location and symbol
            Graphic pinGraphic = new Graphic((MapPoint)mapPoint, compositeSymbol);

            // Add graphic to the graphics overlay
            _sketchOverlay.Graphics.Add(pinGraphic);
        }

        private CompositeSymbol GenerateCompositeSymbolWithTextLabelFromResources(string path, string name)
        {
            // Get image from local resource by path from UI 
            Uri fileUri = new Uri($"pack://application:,,,{path}");

            // Creates marker symbol from uri
            PictureMarkerSymbol pinSymbol = new PictureMarkerSymbol(fileUri)
            {
                OffsetY = 20,
                Width = 40,
                Height = 40
            };

            var textSymbol = new TextSymbol(
                name, Color.Black, 15, Esri.ArcGISRuntime.Symbology.HorizontalAlignment.Center,
                Esri.ArcGISRuntime.Symbology.VerticalAlignment.Top);
            textSymbol.BackgroundColor = Color.DarkGreen;

            var compositeSymbol = new CompositeSymbol();
            compositeSymbol.Symbols.Add(pinSymbol);
            compositeSymbol.Symbols.Add(textSymbol);

            return compositeSymbol;
        }
    }
}
