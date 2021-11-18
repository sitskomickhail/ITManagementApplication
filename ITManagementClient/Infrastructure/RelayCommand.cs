using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using ITManagementClient.Navigation;

namespace ITManagementClient.Infrastructure
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _executeAction;

        public RelayCommand(Action<object> executeAction)
            : this(executeAction, null)
        {
            _executeAction = executeAction;
        }

        public RelayCommand(Action<object> executeAction, Predicate<object> canExecute)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException("executeAction");
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            Task.Run(() =>
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    Mediator.Notify("ShowProgressBar");
                    _executeAction(parameter);
                    Mediator.Notify("HideProgressBar");
                });
            });
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        private event EventHandler CanExecuteChangedInternal;
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChangedInternal.Raise(this);
        }
    }
}