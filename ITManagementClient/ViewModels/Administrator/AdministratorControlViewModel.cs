using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ITManagementClient.Infrastructure;
using ITManagementClient.Models.Enums;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Departments;
using ITManagementClient.ViewModels.Interfaces;
using ITManagementClient.ViewModels.Projects;
using ITManagementClient.ViewModels.Requests;
using ITManagementClient.ViewModels.UserControls;

namespace ITManagementClient.ViewModels.Administrator
{
    public class AdministratorControlViewModel : BaseViewModel, IPageViewModel
    {
        private Dictionary<string, IControlViewModel> _administratorControls;
        public Dictionary<string, IControlViewModel> AdministratorControls => _administratorControls ?? (_administratorControls = new Dictionary<string, IControlViewModel>());

        private IControlViewModel _currentControlViewModel;
        public IControlViewModel CurrentControlViewModel
        {
            get => _currentControlViewModel;
            set
            {
                _currentControlViewModel = value;
                OnPropertyChanged(nameof(CurrentControlViewModel));
            }
        }

        public ICommand ShowWorkerListCommand { get; set; }
        public ICommand ShowWorkerCreationCommand { get; set; }
        public ICommand ManageDepartmentsCommand { get; set; }
        public ICommand ShowProjectsListCommand { get; set; }
        public ICommand ShowRequestCreationCommand { get; set; }
        public ICommand ShowRequestsListCommand { get; set; }

        public AdministratorControlViewModel()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x =>
                typeof(IControlViewModel).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (var viewModel in types)
            {
                var viewModelInstance = (IControlViewModel)viewModel.GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });
                if (viewModelInstance != null && viewModelInstance.PageDefinitions.Any(p => p == PageDefinition.Administrator))
                {
                    AdministratorControls.Add(viewModel.Name, viewModelInstance);
                    Mediator.Subscribe(viewModel.Name, ChangeViewModel);
                }
            }

            ShowWorkerListCommand = new RelayCommand(ShowWorkerListCommandExecute);
            ShowWorkerCreationCommand = new RelayCommand(ShowWorkerCreationCommandExecute);
            ManageDepartmentsCommand = new RelayCommand(ManageDepartmentsCommandExecute);
            ShowProjectsListCommand = new RelayCommand(ShowProjectsListCommandExecute);
            ShowRequestCreationCommand = new RelayCommand(ShowRequestCreationCommandExecute);
            ShowRequestsListCommand = new RelayCommand(ShowRequestsListCommandExecute);
        }

        private void ChangeViewModel(object obj)
        {
            var viewModelName = (string)obj;

            if (!AdministratorControls.Keys.Contains(viewModelName))
                throw new NullReferenceException($"{viewModelName} View Model was not found");

            var viewModel = AdministratorControls[viewModelName];
            var viewModelInstance = (IControlViewModel)viewModel.GetType().GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });

            AdministratorControls[viewModelName] = viewModelInstance;
            Mediator.Subscribe(viewModelName, ChangeViewModel);

            viewModelInstance.LoadInstance();
            CurrentControlViewModel = viewModelInstance;
            Mediator.Notify("RefreshAllControls", this);
        }

        private void ShowWorkerListCommandExecute(object obj)
        {
            Mediator.Notify(nameof(WorkersListViewModel), nameof(WorkersListViewModel));
        }

        private void ShowWorkerCreationCommandExecute(object obj)
        {
            Mediator.Notify(nameof(WorkerCreateViewModel), nameof(WorkerCreateViewModel));
        }

        private void ManageDepartmentsCommandExecute(object obj)
        {
            Mediator.Notify(nameof(ManageDepartmentsViewModel), nameof(ManageDepartmentsViewModel));
        }


        private void ShowProjectsListCommandExecute(object obj)
        {
            Mediator.Notify(nameof(ProjectsListViewModel), nameof(ProjectsListViewModel));
        }

        private void ShowRequestCreationCommandExecute(object obj)
        {
            Mediator.Notify(nameof(RequestCreationViewModel), nameof(RequestCreationViewModel));
        }

        private void ShowRequestsListCommandExecute(object obj)
        {
            Mediator.Notify(nameof(RequestsListViewModel), nameof(RequestsListViewModel));
        }
    }
}