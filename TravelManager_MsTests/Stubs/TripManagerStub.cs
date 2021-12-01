using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Interfaces;

namespace TravelManager_MsTests.Stubs
{
    public class TripManagerStub : ITripGraphicsManager
    {
        public Graphic FakeGraphic { get; set; }
        public List<Graphic> Graphics { get; set; } = new List<Graphic>();
        public MapView Map { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Task<Graphic> DeleteTrip()
        {
            return Task.FromResult(FakeGraphic);
        }

        public void DeleteTripGraphicById(string id)
        {
            var graphic = Graphics.FirstOrDefault(g => g.Attributes["id"] == id);
            Graphics.Remove(graphic);
        }

        public Task<Graphic> DrawTrip(TripType type, string label, string id)
        {
            FakeGraphic.Attributes["id"] = id;
            Graphics.Add(FakeGraphic);
            return Task.FromResult(FakeGraphic);
        }

    }
}
