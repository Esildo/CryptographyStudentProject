using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RelayBase
{
    public abstract class CommandBase: ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private bool _executable = true;
        public bool Executable
        {
            get => _executable;
            set
            {
                if (_executable == value)
                {
                    return;
                }

                _executable = value;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        bool ICommand.CanExecute(object parameter) => Executable && CanExecute(parameter);

        void ICommand.Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                Execute(parameter);
            }
        }

        protected virtual bool CanExecute(object parameter) => true;

        protected abstract void Execute(object parameter);
    }
}
