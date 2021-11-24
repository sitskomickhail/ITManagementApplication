using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ITManagementClient.Infrastructure;
using ITManagementClient.Managers;
using ITManagementClient.Models.Enums;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Administrator;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.CredentialControls;
using ITManagementClient.ViewModels.Interfaces;
using MaterialDesignThemes.Wpf;

namespace ITManagementClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private Dictionary<string, Type> _pageViewModels;
        public Dictionary<string, Type> PageViewModels => _pageViewModels ?? (_pageViewModels = new Dictionary<string, Type>());

        private IPageViewModel _currentPageViewModel;
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

        private Visibility _pVisibility;

        public Visibility ProgressBarVisibility
        {
            get => _pVisibility;
            set { _pVisibility = value; OnPropertyChanged(nameof(ProgressBarVisibility)); }
        }

        public ICommand CloseApplicationCommand { get; set; }

        public ICommand LogoutUserCommand { get; set; }

        public ICommand ShowUserInfoCommand { get; set; }

        public MainWindowViewModel()
        {
            MessageQueue = new SnackbarMessageQueue(new TimeSpan(0, 0, 0, 3));
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x =>
                typeof(IPageViewModel).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            var navigationTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x =>
                typeof(INavigationPageViewModel).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (var viewModel in types)
            {
                //var viewModelInstance = (IPageViewModel)viewModel.GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });

                //if (viewModelInstance != null)
                //{
                    PageViewModels.Add(viewModel.Name, viewModel);
                    Mediator.Subscribe(viewModel.Name, ChangeViewModel);
                //}
            }

            Mediator.Subscribe("SnackbarMessageShow", ShowSnackbar);
            Mediator.Subscribe("EnableUserManagementControls", EnableUserManagementControls);
            Mediator.Subscribe("ShowProgressBar", ShowProgressBar);
            Mediator.Subscribe("HideProgressBar", HideProgressBar);

            WorkerNavElementsVisibility = Visibility.Hidden;
            ProgressBarVisibility = Visibility.Hidden;

            CloseApplicationCommand = new RelayCommand(ShutdownApplication);
            LogoutUserCommand = new RelayCommand(LogoutUserCommandExecute);
            ShowUserInfoCommand = new RelayCommand(ShowUserInfoCommandExecute);

            ChangeViewModel(nameof(LoginControlViewModel));
        }

        private void ChangeViewModel(object obj)
        {
            var viewModelName = (string)obj;

            var viewModel = PageViewModels[viewModelName];
            var viewModelInstance = (IPageViewModel)viewModel.GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });

            Mediator.Subscribe(viewModelName, ChangeViewModel);
            Mediator.Subscribe("RefreshAllControls", RefreshAllControls);

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

        private void EnableUserManagementControls(object obj)
        {
            var connectedUser = UserManager.GetCurrentConnectedUser();

            if (connectedUser != null)
            {
                WorkerNavElementsVisibility = Visibility.Visible;
                switch (connectedUser.Role)
                {
                    case UserRoles.Administrator:
                        Mediator.Notify(nameof(AdministratorControlViewModel), nameof(AdministratorControlViewModel));
                        break;
                    case UserRoles.Developer:
                        //Mediator.Notify(nameof(AdministratorControlViewModel));
                        break;
                    case UserRoles.HrManager:
                        //Mediator.Notify(nameof(AdministratorControlViewModel));
                        break;
                    case UserRoles.ResourceManager:
                        //Mediator.Notify(nameof(AdministratorControlViewModel));
                        break;
                    default:
                        Mediator.Notify("SnackbarMessageShow", "Incorrect role");
                        break;
                }
            }
        }

        private void RefreshAllControls(object obj)
        {
            CurrentPageViewModel = (IPageViewModel)obj;
        }

        private void ShutdownApplication(object obj)
        {
            Application.Current.Shutdown();
        }

        private void LogoutUserCommandExecute(object obj)
        {
            UserManager.DisconnectCurrentUser();
            Mediator.Notify(nameof(LoginControlViewModel), nameof(LoginControlViewModel));
            WorkerNavElementsVisibility = Visibility.Hidden;
        }

        private void ShowUserInfoCommandExecute(object obj)
        {
            var connectedUser = UserManager.GetCurrentConnectedUser();

            if (connectedUser != null)
            {
                //Mediator.Notify(User info popup);
            }
        }

        private void ShowProgressBar(object obj)
        {
            ProgressBarVisibility = Visibility.Visible;
        }

        private void HideProgressBar(object obj)
        {
            ProgressBarVisibility = Visibility.Hidden;
        }
    }
}