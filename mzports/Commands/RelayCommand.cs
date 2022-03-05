using System;
using System.Windows.Input;

namespace mzports.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Action _action;

        public RelayCommand(Action action) => _action = action;
        public bool CanExecute(object? parameter)
        {
            return parameter == null ? true : (bool)parameter;
        }

        public void Execute(object? parameter)
        {
            _action();
        }
    }
}
