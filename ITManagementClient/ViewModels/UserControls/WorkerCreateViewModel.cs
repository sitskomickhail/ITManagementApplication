using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Handlers.Workers;
using ITManagementClient.Helpers;
using ITManagementClient.Infrastructure;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Workers;
using ITManagementClient.Models.ResponseModels.Workers;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;

namespace ITManagementClient.ViewModels.UserControls
{
    public class WorkerCreateViewModel : BaseViewModel, IControlViewModel
    {
        public IEnumerable<PageDefinition> PageDefinitions => new List<PageDefinition> { PageDefinition.Administrator };

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

        private string _login;
        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(nameof(Login)); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _repeatPassword;
        public string RepeatPassword
        {
            get => _repeatPassword;
            set
            {
                _repeatPassword = value;
                OnPropertyChanged(nameof(RepeatPassword));
            }
        }

        public ICommand CreateWorkerCommand { get; set; }

        public BaseActionHandler<CreateNewWorkerRequestModel, CreateNewWorkerResponseModel> CreateWorkerActionHandler { get; set; }

        public WorkerCreateViewModel()
        {
            SelectedRole = RolesList.FirstOrDefault();
            WorkerBirthDate = DateTime.Now.AddYears(-18);
            WorkerSalary = "0";

            CreateWorkerCommand = new RelayCommand(CreateWorkerCommandExecute);
            CreateWorkerActionHandler = new CreateNewWorkerActionHandler();
        }

        private void CreateWorkerCommandExecute(object obj)
        {
            if (!Password.Equals(RepeatPassword))
            {
                Mediator.Notify("SnackbarMessageShow", "Введенные пароли не совпадают!");
                return;
            }

            try
            {
                CreateWorkerActionHandler.ExecuteHandler(new CreateNewWorkerRequestModel
                {
                    Password = Password,
                    Login = Login,
                    BirthDate = WorkerBirthDate,
                    Name = WorkerName,
                    Salary = Decimal.Parse(WorkerSalary),
                    Position = _userRoles[SelectedRole]
                });

                Mediator.Notify(nameof(WorkersListViewModel), nameof(WorkersListViewModel));
            }
            catch { }
        }
    }
}