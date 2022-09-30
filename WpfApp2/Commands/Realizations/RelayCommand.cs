using System;
using System.Collections.Generic;
using System.Text;
using RelayBase;
namespace Relay
{
    internal class RelayCommand: CommandBase
    {

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        protected override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        protected override void Execute(object parameter) => _execute(parameter);
    }
}
