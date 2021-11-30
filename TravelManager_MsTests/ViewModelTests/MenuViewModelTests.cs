using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelManager.Domain.Entities;
using TravelManager.Presentation.ViewModels;
using TravelManager_MsTests.Stubs;

namespace TravelManager_MsTests.ViewModelTests
{
    [TestClass]
    public class MenuViewModelTests
    {

        [TestMethod]
        public async Task MenuViewModel_AddTripCommandExecute_CreatedTripCorrectly_Test()
        {
            //Arrange
            Graphic graphic = new Graphic();
            graphic.Geometry = new MapPoint(30, 50);

            TripManagerStub tripManagerStub = new TripManagerStub();
            tripManagerStub.FakeGraphic = graphic;

            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, new MessageBoxWrapperStub());
            await Task.Run(() => menuViewModel.DrawCommand.Execute(null));

            menuViewModel.Name = "trip1";
            menuViewModel.Description = "trip2";
            menuViewModel.Type = TripType.Park;
            menuViewModel.VisitDate = new DateTime(2025, 1, 1);

            //Act
            menuViewModel.AddTrip.Execute(null);

            //Assert
            Assert.IsTrue(menuViewModel.AllTrips.Count == 1);
            Assert.AreEqual("trip1", menuViewModel.AllTrips[0].Name);
            Assert.AreEqual("trip2", menuViewModel.AllTrips[0].Description);
            Assert.AreEqual(TripType.Park, menuViewModel.AllTrips[0].Type);
            Assert.AreEqual(new DateTime(2025, 1, 1), menuViewModel.AllTrips[0].VisitDate);
            Assert.AreEqual(graphic.Geometry, menuViewModel.AllTrips[0].Point);
            Assert.IsNull(menuViewModel.Name);
            Assert.IsNull(menuViewModel.Description);
            Assert.AreEqual(TripType.Beach, menuViewModel.Type);
            Assert.AreEqual(menuViewModel.VisitDate, DateTime.Today);
        }

        [TestMethod]
        public async Task MenuViewModel_AddTripCommandCanExecute_NameContainsLessThan3CharactersAndGraphicsNull_False_Test()
        {
            //Arrange
            TripManagerStub tripManagerStub = new TripManagerStub();
            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, new MessageBoxWrapperStub());

            await Task.Run(() => menuViewModel.DrawCommand.Execute(null));

            menuViewModel.Name = "tr";

            //Act
            var result = menuViewModel.AddTrip.CanExecute(null);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task MenuViewModel_AddTripCommandCanExecute_NameContainsLessThan3CharactersAndGraphicsHasPoint_False_Test()
        {
            //Arrange
            Graphic graphic = new Graphic();
            graphic.Geometry = new MapPoint(30, 50);

            TripManagerStub tripManagerStub = new TripManagerStub();
            tripManagerStub.FakeGraphic = graphic;
            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, new MessageBoxWrapperStub());

            await Task.Run(() => menuViewModel.DrawCommand.Execute(null));

            menuViewModel.Name = "tr";

            //Act
            var result = menuViewModel.AddTrip.CanExecute(null);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task MenuViewModel_AddTripCommandCanExecute_NameLongerThan3CharactersAndGraphicsNull_False_Test()
        {
            //Arrange
            TripManagerStub tripManagerStub = new TripManagerStub();
            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, new MessageBoxWrapperStub());

            await Task.Run(() => menuViewModel.DrawCommand.Execute(null));

            menuViewModel.Name = "trip";

            //Act
            var result = menuViewModel.AddTrip.CanExecute(null);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task MenuViewModel_AddTripCommandCanExecute_NameLongerThan3CharactersAndGraphicsHasPoint_True_Test()
        {
            //Arrange
            Graphic graphic = new Graphic();
            graphic.Geometry = new MapPoint(30, 50);

            TripManagerStub tripManagerStub = new TripManagerStub();
            tripManagerStub.FakeGraphic = graphic;
            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, new MessageBoxWrapperStub());

            await Task.Run(() => menuViewModel.DrawCommand.Execute(null));

            menuViewModel.Name = "trip";

            //Act
            var result = menuViewModel.AddTrip.CanExecute(null);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task MenuViewModel_DrawCommandCanExecute_GraphicsGetsPoint_False_Test()
        {
            //Arrange
            Graphic graphic = new Graphic();
            graphic.Geometry = new MapPoint(30, 50);
            TripManagerStub tripManagerStub = new TripManagerStub();
            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, new MessageBoxWrapperStub());
            tripManagerStub.FakeGraphic = graphic;

            await Task.Run(() => menuViewModel.DrawCommand.Execute(null));

            //Act
            var result = menuViewModel.DrawCommand.CanExecute(null);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void MenuViewModel_DrawCommandCanExecute_GraphicsIsNull_True_Test()
        {
            //Arrange
            TripManagerStub tripManagerStub = new TripManagerStub();
            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, new MessageBoxWrapperStub());

            //Act
            var result = menuViewModel.DrawCommand.CanExecute(null);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MenuViewModel_RemoveCommandExecute_RemovedTripCorrectly_Test()
        {
            //Arrange
            Graphic graphic = new Graphic();
            graphic.Geometry = new MapPoint(30, 50);

            TripManagerStub tripManagerStub = new TripManagerStub();
            tripManagerStub.FakeGraphic = graphic;

            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, new MessageBoxWrapperStub());
            menuViewModel.DrawCommand.Execute(null);

            menuViewModel.Name = "trip1";
            menuViewModel.Description = "trip2";
            menuViewModel.Type = TripType.Park;
            menuViewModel.VisitDate = new DateTime(2025, 1, 1);

            menuViewModel.AddTrip.Execute(null);

            //Act
            menuViewModel.RemoveTripFromMap.Execute(null);

            //Assert
            Assert.IsTrue(menuViewModel.AllTrips.Count == 0);
            Assert.AreEqual(menuViewModel.CursorType, Cursors.Arrow);
        }

        [TestMethod]
        public void MenuViewModel_RemoveCommandCanExecute_AllTripsEmpty_False_Test()
        {
            //Arrange
            TripManagerStub tripManagerStub = new TripManagerStub();
            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, new MessageBoxWrapperStub());

            //Act
            var result = menuViewModel.RemoveTripFromMap.CanExecute(null);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void MenuViewModel_RemoveCommandCanExecute_AllTripsHasElements_True_Test()
        {
            //Arrange
            Graphic graphic = new Graphic();
            graphic.Geometry = new MapPoint(30, 50);

            TripManagerStub tripManagerStub = new TripManagerStub();
            tripManagerStub.FakeGraphic = graphic;

            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, new MessageBoxWrapperStub());
            menuViewModel.DrawCommand.Execute(null);

            menuViewModel.Name = "trip1";
            menuViewModel.Description = "trip2";
            menuViewModel.Type = TripType.Park;
            menuViewModel.VisitDate = new DateTime(2025, 1, 1);

            menuViewModel.AddTrip.Execute(null);

            //Act
            var result = menuViewModel.RemoveTripFromMap.CanExecute(null);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MenuViewModel_RemoveTripExecute_RemovedTripCorrectly_MessageBoxResult_Yes_Test()
        {
            //Arrange
            Graphic graphic = new Graphic();
            graphic.Geometry = new MapPoint(30, 50);

            MessageBoxWrapperStub messageBoxWrapper = new MessageBoxWrapperStub();
            TripManagerStub tripManagerStub = new TripManagerStub();

            tripManagerStub.FakeGraphic = graphic;
            messageBoxWrapper.Result = DialogResult.Yes;

            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, messageBoxWrapper);
            menuViewModel.DrawCommand.Execute(null);

            menuViewModel.Name = "trip1";
            menuViewModel.Description = "trip2";
            menuViewModel.Type = TripType.Park;
            menuViewModel.VisitDate = new DateTime(2025, 1, 1);
            menuViewModel.AddTrip.Execute(null);
            Assert.IsTrue(tripManagerStub.Graphics.Count == 1);

            //Act
            menuViewModel.RemoveTrip.Execute(menuViewModel.AllTrips[0]);

            //Assert
            Assert.IsTrue(menuViewModel.AllTrips.Count == 0);
            Assert.IsTrue(tripManagerStub.Graphics.Count == 0);
        }

        [TestMethod]
        public void MenuViewModel_RemoveTripExecute_RemovedTripCorrectly_MessageBoxResult_No_Test()
        {
            //Arrange
            Graphic graphic = new Graphic();
            graphic.Geometry = new MapPoint(30, 50);

            MessageBoxWrapperStub messageBoxWrapper = new MessageBoxWrapperStub();
            TripManagerStub tripManagerStub = new TripManagerStub();

            tripManagerStub.FakeGraphic = graphic;
            messageBoxWrapper.Result = DialogResult.No;

            MenuViewModel menuViewModel = new MenuViewModel(tripManagerStub, messageBoxWrapper);
            menuViewModel.DrawCommand.Execute(null);

            menuViewModel.Name = "trip1";
            menuViewModel.Description = "trip2";
            menuViewModel.Type = TripType.Park;
            menuViewModel.VisitDate = new DateTime(2025, 1, 1);
            menuViewModel.AddTrip.Execute(null);
            Assert.IsTrue(tripManagerStub.Graphics.Count == 1);

            //Act
            menuViewModel.RemoveTrip.Execute(menuViewModel.AllTrips[0]);

            //Assert
            Assert.IsTrue(menuViewModel.AllTrips.Count == 1);
            Assert.IsTrue(tripManagerStub.Graphics.Count == 1);
        }
    }
}
