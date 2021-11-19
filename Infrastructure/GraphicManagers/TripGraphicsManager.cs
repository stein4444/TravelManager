using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Interfaces;
using ArcGISHorizontalAlignment = Esri.ArcGISRuntime.Symbology.HorizontalAlignment;
using ArcGISVerticalAlignment = Esri.ArcGISRuntime.Symbology.VerticalAlignment;

namespace TravelManager.Infrastructure.GraphicManagers
{
    public class TripGraphicsManager : ITripGraphicsManager
    {
        private readonly GraphicsOverlay _overlay;
        private readonly SketchEditor _sketchEditor;
        private Dictionary<TripType, Uri> _images;

        public TripGraphicsManager(GraphicsOverlay overlay, SketchEditor sketchEditor)
        {
            _overlay = overlay;
            _sketchEditor = sketchEditor;
            _images = new Dictionary<TripType, Uri>()
            {
                { TripType.Architecture, new Uri($"pack://application:,,,/Images/statue.jpg") },
                { TripType.Beach, new Uri($"pack://application:,,,/Images/beach.png") },
                { TripType.Park, new Uri($"pack://application:,,,/Images/mountains.jpg") }
            };
        }
        public async Task<Graphic> DrawTrip(TripType type, string label, string id)
        {
            try
            {
                Geometry mapPoint = await _sketchEditor.StartAsync(SketchCreationMode.Point, false);

                if (mapPoint != null)
                {
                    var compositeSymbol = CreateTripSymbol(_images[type], label);
                    Graphic tripGraphic = new Graphic((MapPoint)mapPoint, compositeSymbol);
                    tripGraphic.Attributes["id"] = id;
                    _overlay.Graphics.Add(tripGraphic);
                    return tripGraphic;
                }
                return null;
            }
            catch (TaskCanceledException)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private CompositeSymbol CreateTripSymbol(Uri image, string name)
        {
            // Creates marker symbol from uri
            PictureMarkerSymbol pinSymbol = new PictureMarkerSymbol(image)
            {
                OffsetY = 20,
                Width = 40,
                Height = 40
            };

            var textSymbol = new TextSymbol(
                name, Color.Black, 15, ArcGISHorizontalAlignment.Center, ArcGISVerticalAlignment.Top);
            textSymbol.BackgroundColor = Color.DarkGreen;

            var compositeSymbol = new CompositeSymbol();
            compositeSymbol.Symbols.Add(pinSymbol);
            compositeSymbol.Symbols.Add(textSymbol);

            return compositeSymbol;
        }
    }
}
