using System.Collections.Generic;
using TravelManager.ApplicationServices.ViewModels.Base;
using TravelManager.Domain.Entities;

namespace TravelManager.Presentation.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private List<Poi> _points;
        public List<Poi> Points
        {
            get
            {
                if (_points == null)
                {
                    _points = new List<Poi>()
                    {
                        new Poi { ImagePath = "/Images/beach.png", Name = "Beach"},
                        new Poi { ImagePath = "/Images/statue.jpg", Name = "Statue "},
                        new Poi { ImagePath = "/Images/city.jpg", Name = "City "},
                        new Poi { ImagePath = "/Images/country.jpg", Name = "Country "},
                        new Poi { ImagePath = "/Images/mountains.jpg", Name = "Mountains"}
                    };
                }
                return _points;
            }
        }
    }
}
