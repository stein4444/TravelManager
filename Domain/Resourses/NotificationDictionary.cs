using System.Collections.Generic;
using TravelManager.Domain.Entities;

namespace TravelManager.Domain.Resourses
{
    public class NotificationDictionary
    {
        private Dictionary<SpecificNotifications, string> _notificationCollection;

        public Dictionary<SpecificNotifications, string> NotificationCollection
        {
            get
            {
                if (_notificationCollection == null)
                {
                    _notificationCollection = new Dictionary<SpecificNotifications, string>()
                    {
                        { SpecificNotifications.DrawError, "Error! Point can't be drawn please try again" },
                        { SpecificNotifications.DrawInfo, "Success! Poin successfully has been drawn" },
                        { SpecificNotifications.ExportError, "Error! Can't save file" },
                        { SpecificNotifications.ExportInfo, "Success! Successfully saved to file" },
                        { SpecificNotifications.ExportWarning, "Warning! If you don't export the trips it wouldn't be saved" },
                        { SpecificNotifications.SaveInfo, "Success! Trip successfully added to list" },
                        { SpecificNotifications.SaveError, "Error! Trip can't be saved" },
                        { SpecificNotifications.SaveWarning, "Error! Trip can't be saved" }
                    };
                }
                return _notificationCollection;
            }
        }
    }
}
