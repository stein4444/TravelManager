using TravelManager.Domain.Entities;
using TravelManager.Domain.Interfaces;

namespace TravelManager_MsTests.Stubs
{
    public class MessageBoxWrapperStub : IMessageBoxWrapper
    {
        public DialogResult Result { get; set; }

        public DialogResult Show(string msg, string title, DialogType buttons)
        {
            return Result;
        }
    }
}
