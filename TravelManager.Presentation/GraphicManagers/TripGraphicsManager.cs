using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Interfaces;
using TravelManager.Domain.Resourses;
using ArcGISHorizontalAlignment = Esri.ArcGISRuntime.Symbology.HorizontalAlignment;
using ArcGISVerticalAlignment = Esri.ArcGISRuntime.Symbology.VerticalAlignment;
using SystemColor = System.Drawing.Color;

namespace TravelManager.Presentation.GraphicsManger
{
    public class TripGraphicsManager : ITripGraphicsManager
    {
        private readonly GraphicsOverlay _overlay;
        private readonly SketchEditor _sketchEditor;
        private readonly MapView _map;
        private ImageResourse _images;
        private const string TRIP_ID = "id";

        public TripGraphicsManager(MapView map)
        {
            _overlay = map.GraphicsOverlays.Single();
            _sketchEditor = map.SketchEditor;
            _map = map;
            _images = new ImageResourse(); 
        }

        public async Task<Graphic> DrawTrip(TripType type, string label, string id)
        {
            try
            {
                Geometry mapPoint = await _sketchEditor.StartAsync(SketchCreationMode.Point, false);

                if (mapPoint != null)
                {
                    var compositeSymbol = CreateTripSymbol(_images.Images[type], label);

                    Graphic tripGraphic = new Graphic((MapPoint)mapPoint, compositeSymbol);
                    tripGraphic.Attributes[TRIP_ID] = id;

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

        public async Task<Graphic> DeleteTrip()
        {
            try
            {
                var graphic = await GetGraphicAsync();
                _overlay.Graphics.Remove(graphic);
                return graphic;
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

        public void DeleteTripGraphicById(string id)
        {
            var graphic = _overlay.Graphics.FirstOrDefault(g => g.Attributes[TRIP_ID] == id);
            if(graphic == null)
            {
                return;
            }
            _overlay.Graphics.Remove(graphic);
        }

        private async Task<Graphic> GetGraphicAsync()
        {
            Geometry mapPoint = await _map.SketchEditor.StartAsync(SketchCreationMode.Point, false);

            Point screenCoordinate = _map.LocationToScreen((MapPoint)mapPoint);

            IReadOnlyList<IdentifyGraphicsOverlayResult> results = await _map.IdentifyGraphicsOverlaysAsync(screenCoordinate, 2, false);

            Graphic graphic = null;
            IdentifyGraphicsOverlayResult idResult = results.FirstOrDefault();

            if (idResult != null && idResult.Graphics.Count > 0)
            {
                graphic = idResult.Graphics.FirstOrDefault();
            }

            return graphic;
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
                name, SystemColor.Black, 15, ArcGISHorizontalAlignment.Center, ArcGISVerticalAlignment.Top);
            textSymbol.BackgroundColor = SystemColor.DarkGreen;

            var compositeSymbol = new CompositeSymbol();
            compositeSymbol.Symbols.Add(pinSymbol);
            compositeSymbol.Symbols.Add(textSymbol);

            return compositeSymbol;
        }
    }
}
