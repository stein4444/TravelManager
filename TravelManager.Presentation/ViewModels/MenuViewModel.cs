using System.Collections.Generic;
using TravelManager.ApplicationServices.ViewModels.Base;
using TravelManager.Domain.Entities;

namespace TravelManager.Presentation.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public List<Poi> PoiTypes
        {
            get
            {
                return new List<Poi>
            {
                new Poi { ImagePath = "/Images/beach.png", Name = "Beach"},
                new Poi { ImagePath = "/Images/statue.jpg", Name = "Statue "},
                new Poi { ImagePath = "/Images/city.jpg", Name = "City "},
                new Poi { ImagePath = "/Images/country.jpg", Name = "Country "},
                new Poi { ImagePath = "/Images/mountains.jpg", Name = "Mountains"}
            };
            }
        }
    }
}
