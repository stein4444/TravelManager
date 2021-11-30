using System;

namespace TravelManager.Presentation.Commands
{
    public sealed class DelegateCommand : Command
    {
        private static readonly Func<object, bool> defaultCanExecuteMethod = (obj) => true;

        private readonly Func<object, bool> canExecuteMethod;
        private readonly Action<object> executeMethod;

        public DelegateCommand(Action<object> executeMethod) :
            this(executeMethod, defaultCanExecuteMethod)
        {
        }

        public DelegateCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            this.canExecuteMethod = canExecuteMethod;
            this.executeMethod = executeMethod;
        }

        public override bool CanExecute(object parameter)
        {
            return canExecuteMethod(parameter);
        }

        public override void Execute(object parameter)
        {
            executeMethod(parameter);
        }
    }
}
