using TravelManager.Domain.Entities;

namespace TravelManager.Domain.Interfaces
{
    public interface IMessageBoxWrapper
    {
        DialogResult Show(string msg, string title, DialogType buttons);
    }
}
