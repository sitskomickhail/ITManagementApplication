using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;
using ITManagementClient.Infrastructure;
using ITManagementClient.Models.Common.ObservableModels;
using ITManagementClient.Models.Enums;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;

namespace ITManagementClient.ViewModels.UserControls
{
    public class WorkersListViewModel : BaseViewModel, IControlViewModel
    {
        public IEnumerable<PageDefinition> PageDefinitions => new List<PageDefinition> { PageDefinition.Administrator };

        public ObservableCollection<WorkerObservableModel> WorkersList { get; set; }

        private string _workerName;
        public string WorkerName
        {
            get => _workerName;
            set { _workerName = value; OnPropertyChanged(nameof(WorkerName)); }
        }

        public IEnumerable<string> RolesList { get; set; } = new List<string> { "Разработчик", "Ресурсный менеджер", "HR-менеджер", "Системный администратор" };

        private string _selectedRole;
        public string SelectedRole
        {
            get => _selectedRole;
            set { _selectedRole = value; OnPropertyChanged(nameof(SelectedRole)); }
        }

        private string _workerSalary;
        public string WorkerSalary
        {
            get => _workerSalary;
            set
            {
                if (decimal.TryParse(value, out _))
                {
                    _workerSalary = value;
                    OnPropertyChanged(nameof(WorkerSalary));
                }
            }
        }

        private DateTime _workerBirthDate;
        public DateTime WorkerBirthDate
        {
            get => _workerBirthDate;
            set { _workerBirthDate = value; OnPropertyChanged(nameof(WorkerBirthDate)); }
        }

        private DateTime _workerHireDate;
        public DateTime WorkerHireDate
        {
            get => _workerHireDate;
            set { _workerHireDate = value; OnPropertyChanged(nameof(WorkerHireDate)); }
        }

        private bool _workerAccountActive;
        public bool WorkerAccountActive
        {
            get => _workerAccountActive;
            set { _workerAccountActive = value; OnPropertyChanged(nameof(WorkerAccountActive)); }
        }

        private string _workerEnglishLevel;
        public string WorkerEnglishLevel
        {
            get => _workerEnglishLevel;
            set { _workerEnglishLevel = value; OnPropertyChanged(nameof(WorkerEnglishLevel)); }
        }

        private string _workerDepartmentName;
        public string WorkerDepartmentName
        {
            get => _workerDepartmentName;
            set { _workerDepartmentName = value; OnPropertyChanged(nameof(WorkerDepartmentName)); }
        }

        private string _login;
        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(nameof(Login)); }
        }

        private string _searchParameter;
        public string SearchParameter
        {
            get => _searchParameter;
            set { _searchParameter = value; OnPropertyChanged(nameof(SearchParameter)); }
        }

        public ICommand SearchByParameterCommand { get; set; }
        public ICommand SaveUpdatedWorkerCommand { get; set; }

        public WorkersListViewModel()
        {
            WorkersList = new ObservableCollection<WorkerObservableModel>();

            WorkersList.Add(new WorkerObservableModel()
            {
                Number = 1,
                Name = "Тест 1",
                Department = "Python",
                HireDate = "12.12.2020",
                Salary = 1100,
                WorkerId = 5,
                ShowWorkerDetailsCommand = new RelayCommand(ShowWorkerDetailsCommandExecute)
            });

            WorkersList.Add(new WorkerObservableModel()
            {
                Number = 2,
                Name = "Тест 2",
                Department = "",
                HireDate = "26.05.2020",
                Salary = 2500.60m,
                WorkerId = 10,
                ShowWorkerDetailsCommand = new RelayCommand(ShowWorkerDetailsCommandExecute)
            });

            SaveUpdatedWorkerCommand = new RelayCommand(SaveUpdatedWorkerCommandExecute);
            SearchByParameterCommand = new RelayCommand(SearchByParameterCommandExecute);
        }

        private void ShowWorkerDetailsCommandExecute(object obj)
        {

        }

        private void SaveUpdatedWorkerCommandExecute(object obj)
        {

        }

        private void SearchByParameterCommandExecute(object obj)
        {

        }
    }
}