using System.Windows;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Interfaces;

namespace TravelManager.Presentation
{
    public class MessageBoxWrapper : IMessageBoxWrapper
    {
        public DialogResult Show(string msg, string title, DialogType buttons)
        {
            var messageBoxButtons = buttons == DialogType.YesNo ?
                MessageBoxButton.YesNo : MessageBoxButton.OK;

            var result = MessageBox.Show(msg, title, messageBoxButtons);

            return result == MessageBoxResult.Yes ?
                DialogResult.Yes : DialogResult.No; 
        }
    }
}
