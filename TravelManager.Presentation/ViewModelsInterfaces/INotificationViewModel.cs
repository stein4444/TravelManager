using TravelManager.Domain.Entities;

namespace TravelManager.Presentation.ViewModelsInterfaces
{
    public interface INotificationViewModel
    {
        NotificationType NotificationType { get; set; }
        string Message { get; set; }
    }
}
