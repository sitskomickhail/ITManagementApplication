using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ITManagementClient.Infrastructure;
using ITManagementClient.Managers;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;
using ITManagementClient.ViewModels.UserControls;
using MaterialDesignThemes.Wpf;

namespace ITManagementClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IPageViewModel _currentPageViewModel;
        private Dictionary<string, IPageViewModel> _pageViewModels;

        public Dictionary<string, IPageViewModel> PageViewModels => _pageViewModels ?? (_pageViewModels = new Dictionary<string, IPageViewModel>());

        public IPageViewModel CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }

        private ISnackbarMessageQueue _messageQueue;
        public ISnackbarMessageQueue MessageQueue
        {
            get => _messageQueue;
            set { _messageQueue = value; OnPropertyChanged(nameof(MessageQueue)); }
        }

        private Visibility _workerNavElementsVisibility;
        public Visibility WorkerNavElementsVisibility
        {
            get => _workerNavElementsVisibility;
            set { _workerNavElementsVisibility = value; OnPropertyChanged(nameof(WorkerNavElementsVisibility)); }
        }

        public ICommand CloseApplicationCommand { get; set; }

        public MainWindowViewModel()
        {
            MessageQueue = new SnackbarMessageQueue(new TimeSpan(0, 0, 0, 3));
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x =>
                typeof(IPageViewModel).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (var viewModel in types)
            {
                var viewModelInstance = (IPageViewModel)viewModel.GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });
                if (viewModelInstance != null)
                {
                    PageViewModels.Add(viewModel.Name, viewModelInstance);
                    Mediator.Subscribe(viewModel.Name, ChangeViewModel);
                }
            }

            CurrentPageViewModel = PageViewModels[nameof(LoginControlViewModel)];
            Mediator.Subscribe("SnackbarMessageShow", ShowSnackbar);
            Mediator.Subscribe("EnableUserManagementElements", ShowUserManagementElements);
            Mediator.Subscribe("DisableUserManagementElements", HideUserManagementElements);

            WorkerNavElementsVisibility = Visibility.Hidden;
            CloseApplicationCommand = new RelayCommand(ShutdownApplication);
        }

        private void ChangeViewModel(object obj)
        {
            var viewModelName = (string)obj;

            if (!PageViewModels.Keys.Contains(viewModelName))
                throw new NullReferenceException($"{viewModelName} View Model was not found");

            var viewModel = PageViewModels[viewModelName];
            var viewModelInstance = (IPageViewModel)viewModel.GetType().GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });

            PageViewModels[viewModelName] = viewModelInstance;
            Mediator.Subscribe(viewModelName, ChangeViewModel);

            CurrentPageViewModel = viewModelInstance;
        }

        private void ShowSnackbar(object obj)
        {
            if (obj != null)
            {
                string message = (string)obj;
                MessageQueue.Enqueue(message);
            }
        }

        private void ShowUserManagementElements(object obj)
        {
            WorkerNavElementsVisibility = Visibility.Visible;
        }

        private void HideUserManagementElements(object obj)
        {
            WorkerNavElementsVisibility = Visibility.Hidden;
        }

        private void ShutdownApplication(object obj)
        {
            Application.Current.Shutdown();
        }
    }
}