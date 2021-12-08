using System.Collections.Generic;
using TravelManager.Domain.Entities;

namespace TravelManager.Domain.Interfaces
{
    public interface IFileSaver<T> 
    {
        public SpecificNotifications Save(IEnumerable<T> genericList);
    }
}
