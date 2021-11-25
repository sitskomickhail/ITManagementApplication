using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using ITManagementClient.Infrastructure;
using ITManagementClient.Models.Enums;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;
using ITManagementClient.ViewModels.Projects;
using ITManagementClient.ViewModels.Requests;
using ITManagementClient.ViewModels.UserControls;

namespace ITManagementClient.ViewModels.ResourceManager
{
    public class ResourceManagerViewModel : BaseViewModel, IPageViewModel
    {
        private Dictionary<string, IControlViewModel> _resourceManagerControls;
        public Dictionary<string, IControlViewModel> ResourceManagerControls => _resourceManagerControls ?? (_resourceManagerControls = new Dictionary<string, IControlViewModel>());

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
        public ICommand ShowRequestCreationCommand { get; set; }
        public ICommand ShowProjectsListCommand { get; set; }
        public ICommand ProjectCreationCommand { get; set; }

        public ResourceManagerViewModel()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x =>
                typeof(IControlViewModel).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (var viewModel in types)
            {
                var viewModelInstance = (IControlViewModel)viewModel.GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });
                if (viewModelInstance != null && viewModelInstance.PageDefinitions.Any(p => p == PageDefinition.ResourceManager))
                {
                    ResourceManagerControls.Add(viewModel.Name, viewModelInstance);
                    Mediator.Subscribe(viewModel.Name, ChangeViewModel);
                }
            }

            ShowWorkerListCommand = new RelayCommand(ShowWorkerListCommandExecute);
            ShowRequestCreationCommand = new RelayCommand(ShowRequestCreationCommandExecute);
            ShowProjectsListCommand = new RelayCommand(ShowProjectsListCommandExecute);
            ProjectCreationCommand = new RelayCommand(ProjectCreationCommandExecute);
        }

        private void ChangeViewModel(object obj)
        {
            var viewModelName = (string)obj;

            if (!ResourceManagerControls.Keys.Contains(viewModelName))
                throw new NullReferenceException($"{viewModelName} View Model was not found");

            var viewModel = ResourceManagerControls[viewModelName];
            var viewModelInstance = (IControlViewModel)viewModel.GetType().GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });

            ResourceManagerControls[viewModelName] = viewModelInstance;
            Mediator.Subscribe(viewModelName, ChangeViewModel);

            viewModelInstance.LoadInstance();
            CurrentControlViewModel = viewModelInstance;
            Mediator.Notify("RefreshAllControls", this);
        }

        private void ShowWorkerListCommandExecute(object obj)
        {
            Mediator.Notify(nameof(WorkersListViewModel), nameof(WorkersListViewModel));
        }

        private void ShowRequestCreationCommandExecute(object obj)
        {
            Mediator.Notify(nameof(RequestCreationViewModel), nameof(RequestCreationViewModel));
        }

        private void ShowProjectsListCommandExecute(object obj)
        {
            Mediator.Notify(nameof(ResourceProjectsListViewModel), nameof(ResourceProjectsListViewModel));
        }

        private void ProjectCreationCommandExecute(object obj)
        {
            Mediator.Notify(nameof(ProjectCreationViewModel), nameof(ProjectCreationViewModel));
        }
    }
}