using System;
using System.Windows.Input;

namespace AirportManagerSystem.Commands
{
    public class RelayCommand : ICommand
    {
        Action handler;

        public RelayCommand(Action h)
        {
            handler = h;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            handler();
        }
    }
}
