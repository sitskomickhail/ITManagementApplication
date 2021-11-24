using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Handlers.Workers;
using ITManagementClient.Helpers;
using ITManagementClient.Infrastructure;
using ITManagementClient.Models.Common.ObservableModels;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Workers;
using ITManagementClient.Models.ResponseModels.Workers;
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

        private Dictionary<string, UserRoles> _userRoles = new Dictionary<string, UserRoles>()
        {
            {UserRoles.Administrator.GetDescription(), UserRoles.Administrator},
            {UserRoles.Developer.GetDescription(), UserRoles.Developer},
            {UserRoles.HrManager.GetDescription(), UserRoles.HrManager},
            {UserRoles.ResourceManager.GetDescription(), UserRoles.ResourceManager}
        };

        public IEnumerable<string> RolesList { get; set; } = new List<string>
        {
            UserRoles.Administrator.GetDescription(),
            UserRoles.Developer.GetDescription(),
            UserRoles.HrManager.GetDescription(),
            UserRoles.ResourceManager.GetDescription()
        };

        private string _selectedRole;
        public string SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;


                OnPropertyChanged(nameof(SelectedRole));
            }
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

        public IEnumerable<string> EnglishLevelsList { get; set; } = new List<string> { "A1", "A2", "B1", "B2", "C1", "C2" };

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

        private int _editingWorkerId;
        public int EditingWorkerId
        {
            get => _editingWorkerId;
            set { _editingWorkerId = value; OnPropertyChanged(nameof(EditingWorkerId)); }
        }

        public ICommand SearchByParameterCommand { get; set; }
        public ICommand SaveUpdatedWorkerCommand { get; set; }

        public BaseActionHandler<UpdateWorkerRequestModel, UpdateWorkerResponseModel> UpdateWorkerActionHandler { get; set; }
        public BaseActionHandler<GetWorkerByIdRequestModel, GetWorkerByIdResponseModel> GetWorkerByIdActionHandler { get; set; }
        public BaseActionHandler<GetWorkerListRequestModel, GetWorkerListResponseModel> GetWorkersListActionHandler { get; set; }

        public WorkersListViewModel()
        {
            WorkersList = new ObservableCollection<WorkerObservableModel>();
            WorkersList.Add(new WorkerObservableModel() { ShowWorkerDetailsCommand = new RelayCommand(ShowWorkerDetailsCommandExecute) });

            SearchParameter = String.Empty;

            SaveUpdatedWorkerCommand = new RelayCommand(SaveUpdatedWorkerCommandExecute);
            SearchByParameterCommand = new RelayCommand(SearchByParameterCommandExecute);

            UpdateWorkerActionHandler = new UpdateWorkerActionHandler();
            GetWorkerByIdActionHandler = new GetWorkerByIdActionHandler();
            GetWorkersListActionHandler = new GetWorkersListActionHandler();
        }

        public void LoadInstance()
        {
            SearchByParameterCommand.Execute(null);
        }

        private void ShowWorkerDetailsCommandExecute(object obj)
        {
            try
            {
                int workerId = (int)obj;
                var actionResult = GetWorkerByIdActionHandler.ExecuteHandler(new GetWorkerByIdRequestModel
                {
                    WorkerId = workerId
                });

                Login = actionResult.Login;
                EditingWorkerId = actionResult.WorkerId;
                WorkerEnglishLevel = actionResult.EnglishLevel;
                WorkerAccountActive = actionResult.Active;
                WorkerBirthDate = actionResult.BirthDate ?? DateTime.Now;
                WorkerDepartmentName = actionResult.Department;
                WorkerHireDate = actionResult.HireDate;
                WorkerName = actionResult.Name;
                WorkerSalary = actionResult.Salary.ToString();
                SelectedRole = ((UserRoles)actionResult.Position).GetDescription();
            }
            catch { /**/ }
        }

        private void SaveUpdatedWorkerCommandExecute(object obj)
        {
            try
            {
                UpdateWorkerActionHandler.ExecuteHandler(new UpdateWorkerRequestModel()
                {
                    Login = Login,
                    Name = WorkerName,
                    Active = WorkerAccountActive,
                    BirthDate = WorkerBirthDate,
                    EnglishLevel = WorkerEnglishLevel,
                    Position = _userRoles[SelectedRole],
                    Salary = Decimal.Parse(WorkerSalary),
                    WorkerId = EditingWorkerId
                });

                SearchByParameterCommand.Execute(null);
            }
            catch { /**/ }
        }

        private void SearchByParameterCommandExecute(object obj)
        {
            try
            {
                var actionResult = GetWorkersListActionHandler.ExecuteHandler(new GetWorkerListRequestModel
                {
                    SearchParameter = SearchParameter
                });

                Application.Current.Dispatcher.Invoke(() =>
                {
                    WorkersList.Clear();
                    int listCounter = 1;
                    foreach (var worker in actionResult.WorkersList)
                    {
                        WorkersList.Add(new WorkerObservableModel
                        {
                            Name = worker.Name,
                            WorkerId = worker.WorkerId,
                            Salary = worker.Salary,
                            HireDate = worker.HireDate.ToString("dd.MM.yyyy"),
                            Department = worker.Department,
                            Number = listCounter++,
                            ShowWorkerDetailsCommand = new RelayCommand(ShowWorkerDetailsCommandExecute)
                        });
                    }
                });
            }
            catch { /**/ }
        }
    }
}