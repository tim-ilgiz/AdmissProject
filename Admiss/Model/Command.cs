using System;
using System.Windows.Input;

namespace Admiss.Model
{
    public class Command : ICommand
    {
        private Action<object> action;
        private Func<object, bool> func;

        public Command(Action<object> action, Func<object, bool> func = null)
        {
            this.action = action;
            this.func = func;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.func == null || this.func(parameter);
        }

        public void Execute(object parameter)
        {
            this.action(parameter);
        }
    }
}