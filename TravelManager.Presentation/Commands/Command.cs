using System;
using System.Windows.Input;

namespace TravelManager.Presentation.Commands
{
    internal abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        protected virtual bool CanExecute(object parameter) => true;

        protected abstract void Execute(object parameter);

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }
    }
}
