using System;
using System.Windows.Input;


namespace Acr.UserDialogs
{
    public class Command : ICommand
    {
        readonly Action action;


        public Command(Action action)
        {
            this.action = action;
        }


        public bool CanExecute(object parameter)
        {
            return this.IsExecuteable;
        }


        public void Execute(object parameter)
        {
            this.action();
        }


        bool execute = true;

        public bool IsExecuteable
        {
            get { return this.execute; }
            set
            {
                if (this.execute == value)
                    return;

                this.execute = value;
                this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        public event EventHandler CanExecuteChanged;
    }
}
