using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ITManagementClient.Infrastructure;
using ITManagementClient.Models.Enums;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;
using ITManagementClient.ViewModels.Requests;
using ITManagementClient.ViewModels.UserControls;

namespace ITManagementClient.ViewModels.HrManager
{
    public class HrManagerControlViewModel : BaseViewModel, IPageViewModel
    {
        private Dictionary<string, IControlViewModel> _hrManagerControls;
        public Dictionary<string, IControlViewModel> HrManagerControls => _hrManagerControls ?? (_hrManagerControls = new Dictionary<string, IControlViewModel>());

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
        public ICommand ShowRequestsListCommand { get; set; }

        public HrManagerControlViewModel()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x =>
                typeof(IControlViewModel).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (var viewModel in types)
            {
                var viewModelInstance = (IControlViewModel)viewModel.GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });
                if (viewModelInstance != null && viewModelInstance.PageDefinitions.Any(p => p == PageDefinition.HumanResource))
                {
                    HrManagerControls.Add(viewModel.Name, viewModelInstance);
                    Mediator.Subscribe(viewModel.Name, ChangeViewModel);
                }
            }

            ShowWorkerListCommand = new RelayCommand(ShowWorkerListCommandExecute);
            ShowRequestCreationCommand = new RelayCommand(ShowRequestCreationCommandExecute);
            ShowRequestsListCommand = new RelayCommand(ShowRequestsListCommandExecute);
        }

        private void ChangeViewModel(object obj)
        {
            var viewModelName = (string)obj;

            if (!HrManagerControls.Keys.Contains(viewModelName))
                throw new NullReferenceException($"{viewModelName} View Model was not found");

            var viewModel = HrManagerControls[viewModelName];
            var viewModelInstance = (IControlViewModel)viewModel.GetType().GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });

            HrManagerControls[viewModelName] = viewModelInstance;
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

        private void ShowRequestsListCommandExecute(object obj)
        {
            Mediator.Notify(nameof(RequestsListViewModel), nameof(RequestsListViewModel));
        }
    }
}