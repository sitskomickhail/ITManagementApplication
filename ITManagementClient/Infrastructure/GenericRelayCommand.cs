using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ITManagementClient.Infrastructure
{
    public class GenericRelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _executeAction;

        public GenericRelayCommand(Action<T> executeAction) : this(executeAction, null)
        {
            _executeAction = executeAction;
        }

        public GenericRelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _executeAction = execute ?? throw new ArgumentNullException("executeAction");
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
