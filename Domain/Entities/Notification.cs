namespace TravelManager.Domain.Entities
{
    public class Notification
    {
        public SpecificNotifications Type { get; set; }

        public string Message { get; set; }
    }
}
