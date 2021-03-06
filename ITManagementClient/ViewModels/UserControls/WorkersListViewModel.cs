using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Handlers.Departments;
using ITManagementClient.Handlers.Workers;
using ITManagementClient.Helpers;
using ITManagementClient.Infrastructure;
using ITManagementClient.Managers;
using ITManagementClient.Models.Common.ObservableModels;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Departments;
using ITManagementClient.Models.RequestModels.Workers;
using ITManagementClient.Models.ResponseModels.Departments;
using ITManagementClient.Models.ResponseModels.Workers;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;

namespace ITManagementClient.ViewModels.UserControls
{
    public class WorkersListViewModel : BaseViewModel, IControlViewModel
    {
        public IEnumerable<PageDefinition> PageDefinitions => new List<PageDefinition>
        {
            PageDefinition.Administrator, PageDefinition.HumanResource,
            PageDefinition.ResourceManager, PageDefinition.Developer
        };

        public ObservableCollection<WorkerObservableModel> WorkersList { get; set; }

        private string _workerName;
        public string WorkerName
        {
            get => _workerName;
            set { _workerName = value; OnPropertyChanged(nameof(WorkerName)); }
        }

        private readonly IEnumerable<UserRoles> WorkerNameEnableRoles = new List<UserRoles> { UserRoles.Administrator, UserRoles.HrManager, UserRoles.ResourceManager };
        private bool _isWorkerNameEnabled;
        public bool IsWorkerNameEnabled
        {
            get => _isWorkerNameEnabled;
            set
            {
                _isWorkerNameEnabled = value;
                OnPropertyChanged(nameof(IsWorkerNameEnabled));
            }
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

        private readonly IEnumerable<UserRoles> SelectingRolesEnableRoles = new List<UserRoles> { UserRoles.Administrator };
        private bool _isSelectingRoleEnabled;
        public bool IsSelectingRoleEnabled
        {
            get => _isSelectingRoleEnabled;
            set
            {
                _isSelectingRoleEnabled = value;
                OnPropertyChanged(nameof(IsSelectingRoleEnabled));
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

        private readonly IEnumerable<UserRoles> WorkerSalaryEnableRoles = new List<UserRoles> { UserRoles.Administrator, UserRoles.ResourceManager };
        private bool _isWorkerSalaryEnabled;
        public bool IsWorkerSalaryEnabled
        {
            get => _isWorkerSalaryEnabled;
            set
            {
                _isWorkerSalaryEnabled = value;
                OnPropertyChanged(nameof(IsWorkerSalaryEnabled));
            }
        }

        private Visibility _salaryVisibility;
        public Visibility SalaryVisibility
        {
            get => _salaryVisibility;
            set { _salaryVisibility = value; OnPropertyChanged(nameof(SalaryVisibility)); }
        }

        private DateTime _workerBirthDate;
        public DateTime WorkerBirthDate
        {
            get => _workerBirthDate;
            set { _workerBirthDate = value; OnPropertyChanged(nameof(WorkerBirthDate)); }
        }

        private readonly IEnumerable<UserRoles> WorkerBirthDateEnableRoles = new List<UserRoles> { UserRoles.Administrator, UserRoles.HrManager };
        private bool _isWorkerBirthDateEnabled;
        public bool IsWorkerBirthDateEnabled
        {
            get => _isWorkerBirthDateEnabled;
            set
            {
                _isWorkerBirthDateEnabled = value;
                OnPropertyChanged(nameof(IsWorkerBirthDateEnabled));
            }
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

        private readonly IEnumerable<UserRoles> WorkerAccountActiveEnableRoles = new List<UserRoles> { UserRoles.Administrator };
        private bool _isWorkerAccountActiveEnabled;
        public bool IsWorkerAccountActiveEnabled
        {
            get => _isWorkerAccountActiveEnabled;
            set
            {
                _isWorkerAccountActiveEnabled = value;
                OnPropertyChanged(nameof(IsWorkerAccountActiveEnabled));
            }
        }

        public IEnumerable<string> EnglishLevelsList { get; set; } = new List<string> { "A1", "A2", "B1", "B2", "C1", "C2" };

        private string _workerEnglishLevel;
        public string WorkerEnglishLevel
        {
            get => _workerEnglishLevel;
            set { _workerEnglishLevel = value; OnPropertyChanged(nameof(WorkerEnglishLevel)); }
        }

        private readonly IEnumerable<UserRoles> WorkerEnglishLevelEnableRoles = new List<UserRoles> { UserRoles.Administrator, UserRoles.ResourceManager, UserRoles.HrManager };
        private bool _isWorkerEnglishLevelEnabled;
        public bool IsWorkerEnglishLevelEnabled
        {
            get => _isWorkerEnglishLevelEnabled;
            set
            {
                _isWorkerEnglishLevelEnabled = value;
                OnPropertyChanged(nameof(IsWorkerEnglishLevelEnabled));
            }
        }

        private Dictionary<string, int> _departments;

        public ObservableCollection<string> DepartmentsList { get; set; }

        private string _workerDepartmentName;
        public string WorkerDepartmentName
        {
            get => _workerDepartmentName;
            set { _workerDepartmentName = value; OnPropertyChanged(nameof(WorkerDepartmentName)); }
        }

        private readonly IEnumerable<UserRoles> WorkerDepartmentEnableRoles = new List<UserRoles> { UserRoles.ResourceManager };
        private bool _isWorkerDepartmentEnabled;
        public bool IsWorkerDepartmentEnabled
        {
            get => _isWorkerDepartmentEnabled;
            set
            {
                _isWorkerDepartmentEnabled = value;
                OnPropertyChanged(nameof(IsWorkerDepartmentEnabled));
            }
        }

        private readonly IEnumerable<UserRoles> SaveButtonEnableRoles = new List<UserRoles> { UserRoles.Administrator, UserRoles.ResourceManager, UserRoles.HrManager };
        private bool _isSaveButtonEnabled;
        public bool IsSaveButtonEnabled
        {
            get => _isSaveButtonEnabled;
            set
            {
                _isSaveButtonEnabled = value;
                OnPropertyChanged(nameof(IsSaveButtonEnabled));
            }
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
        public ICommand GetAvailableDepartmentsCommand { get; set; }

        public BaseActionHandler<UpdateWorkerRequestModel, UpdateWorkerResponseModel> UpdateWorkerActionHandler { get; set; }
        public BaseActionHandler<GetWorkerByIdRequestModel, GetWorkerByIdResponseModel> GetWorkerByIdActionHandler { get; set; }
        public BaseActionHandler<GetWorkerListRequestModel, GetWorkerListResponseModel> GetWorkersListActionHandler { get; set; }
        public BaseActionHandler<GetDepartmentsListRequestModel, GetDepartmentsListResponseModel> GetDepartmentsListActionHandler { get; set; }

        public WorkersListViewModel()
        {
            WorkersList = new ObservableCollection<WorkerObservableModel>();
            DepartmentsList = new ObservableCollection<string>();
            WorkersList.Add(new WorkerObservableModel() { ShowWorkerDetailsCommand = new RelayCommand(ShowWorkerDetailsCommandExecute) });

            SearchParameter = String.Empty;
            _departments = new Dictionary<string, int>();

            SaveUpdatedWorkerCommand = new RelayCommand(SaveUpdatedWorkerCommandExecute);
            SearchByParameterCommand = new RelayCommand(SearchByParameterCommandExecute);
            GetAvailableDepartmentsCommand = new RelayCommand(GetAvailableDepartmentsCommandExecute);

            UpdateWorkerActionHandler = new UpdateWorkerActionHandler();
            GetWorkerByIdActionHandler = new GetWorkerByIdActionHandler();
            GetWorkersListActionHandler = new GetWorkersListActionHandler();
            GetDepartmentsListActionHandler = new GetDepartmentsListActionHandler();
        }

        public void LoadInstance()
        {
            var currentRole = UserManager.GetCurrentConnectedUser().Role;

            IsSaveButtonEnabled = SaveButtonEnableRoles.Contains(currentRole);
            IsWorkerSalaryEnabled = WorkerSalaryEnableRoles.Contains(currentRole);
            IsSelectingRoleEnabled = SelectingRolesEnableRoles.Contains(currentRole);
            IsWorkerAccountActiveEnabled = WorkerAccountActiveEnableRoles.Contains(currentRole);
            IsWorkerBirthDateEnabled = WorkerBirthDateEnableRoles.Contains(currentRole);
            IsWorkerDepartmentEnabled = WorkerDepartmentEnableRoles.Contains(currentRole);
            IsWorkerEnglishLevelEnabled = WorkerEnglishLevelEnableRoles.Contains(currentRole);
            IsWorkerNameEnabled = WorkerNameEnableRoles.Contains(currentRole);

            SalaryVisibility = WorkerSalaryEnableRoles.Contains(currentRole) ? Visibility.Visible : Visibility.Hidden;

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

                GetAvailableDepartmentsCommand.Execute(null);

                Thread.Sleep(100);

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
                    WorkerId = EditingWorkerId,
                    DepartmentId = String.IsNullOrEmpty(WorkerDepartmentName) ? new int?() : _departments[WorkerDepartmentName]
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
                            Department = worker.Department ?? String.Empty,
                            Number = listCounter++,
                            ShowWorkerDetailsCommand = new RelayCommand(ShowWorkerDetailsCommandExecute),
                            SalaryColumnVisibility = SalaryVisibility
                        });
                    }
                });
            }
            catch { /**/ }
        }

        private void GetAvailableDepartmentsCommandExecute(object obj)
        {
            var actionResult = GetDepartmentsListActionHandler.ExecuteHandler(new GetDepartmentsListRequestModel
            {
                SearchParameter = SearchParameter
            });

            Application.Current.Dispatcher.Invoke(() =>
            {
                DepartmentsList.Clear();
                _departments.Clear();
                DepartmentsList.Add(String.Empty);
                foreach (var department in actionResult.Departments)
                {
                    DepartmentsList.Add(department.Title);
                    _departments.Add(department.Title, department.DepartmentId);
                }
            });

        }
    }
}