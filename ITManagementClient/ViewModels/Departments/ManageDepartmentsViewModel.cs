using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Handlers.Departments;
using ITManagementClient.Infrastructure;
using ITManagementClient.Models.Common.ObservableModels;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Departments;
using ITManagementClient.Models.ResponseModels.Departments;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;

namespace ITManagementClient.ViewModels.Departments
{
    public class ManageDepartmentsViewModel : BaseViewModel, IControlViewModel
    {
        public IEnumerable<PageDefinition> PageDefinitions => new List<PageDefinition> { PageDefinition.Administrator };

        private string _updatingTitle;
        public string UpdatingTitle
        {
            get => _updatingTitle;
            set { _updatingTitle = value; OnPropertyChanged(nameof(UpdatingTitle)); }
        }

        private string _updatingWorkersDuties;
        public string UpdatingWorkersDuties
        {
            get => _updatingWorkersDuties;
            set { _updatingWorkersDuties = value; OnPropertyChanged(nameof(UpdatingWorkersDuties)); }
        }

        private int _updatingDepartmentId;
        public int UpdatingDepartmentId
        {
            get => _updatingDepartmentId;
            set { _updatingDepartmentId = value; OnPropertyChanged(nameof(UpdatingDepartmentId)); }
        }

        private Visibility _removeDepartmentButtonVisibility;
        public Visibility RemoveDepartmentButtonVisibility
        {
            get => _removeDepartmentButtonVisibility;
            set { _removeDepartmentButtonVisibility = value; OnPropertyChanged(nameof(RemoveDepartmentButtonVisibility)); }
        }

        private string _creatingTitle;
        public string CreatingTitle
        {
            get => _creatingTitle;
            set { _creatingTitle = value; OnPropertyChanged(nameof(CreatingTitle)); }
        }

        private string _creatingWorkersDuties;
        public string CreatingWorkersDuties
        {
            get => _creatingWorkersDuties;
            set { _creatingWorkersDuties = value; OnPropertyChanged(nameof(CreatingWorkersDuties)); }
        }

        private string _searchParameter;
        public string SearchParameter
        {
            get => _searchParameter;
            set { _searchParameter = value; OnPropertyChanged(nameof(SearchParameter)); }
        }

        public ObservableCollection<WorkerObservableModel> DepartmentWorkers { get; set; }
        public ObservableCollection<DepartmentObservableModel> DepartmentsList { get; set; }

        public ICommand GetDepartmentsListCommand { get; set; }
        public ICommand UpdateDepartmentCommand { get; set; }
        public ICommand CreateDepartmentCommand { get; set; }
        public ICommand RemoveDepartmentCommand { get; set; }

        public BaseActionHandler<GetDepartmentsListRequestModel, GetDepartmentsListResponseModel> GetDepartmentsListActionHandler { get; set; }
        public BaseActionHandler<CreateDepartmentsRequestModel, CreateDepartmentsResponseModel> CreateDepartmentActionHandler { get; set; }
        public BaseActionHandler<GetFullDepartmentInfoRequestModel, GetFullDepartmentInfoResponseModel> GetFullDepartmentInfoActionHandler { get; set; }
        public BaseActionHandler<DeleteDepartmentRequestModel, DeleteDepartmentResponseModel> RemoveDepartmentActionHandler { get; set; }
        public BaseActionHandler<UpdateDepartmentRequestModel, UpdateDepartmentResponseModel> UpdateDepartmentActionHandler { get; set; }

        public ManageDepartmentsViewModel()
        {
            SearchParameter = String.Empty;

            DepartmentsList = new ObservableCollection<DepartmentObservableModel>();
            DepartmentWorkers = new ObservableCollection<WorkerObservableModel>();
            
            DepartmentsList.Add(new DepartmentObservableModel { GetDepartmentFullInfoCommand = new RelayCommand(GetFullDepartmentInfoCommandExecute) });

            GetDepartmentsListActionHandler = new GetDepartmentsListActionHandler();
            CreateDepartmentActionHandler = new CreateDepartmentActionHandler();
            GetFullDepartmentInfoActionHandler = new GetFullDepartmentInfoActionHandler();
            RemoveDepartmentActionHandler = new RemoveDepartmentActionHandler();
            UpdateDepartmentActionHandler = new UpdateDepartmentActionHandler();

            GetDepartmentsListCommand = new RelayCommand(GetDepartmentsListCommandExecute);
            UpdateDepartmentCommand = new RelayCommand(UpdateDepartmentCommandExecute);
            CreateDepartmentCommand = new RelayCommand(CreateDepartmentCommandExecute);
            RemoveDepartmentCommand = new RelayCommand(RemoveDepartmentCommandExecute);
        }

        public void LoadInstance()
        {
            GetDepartmentsListCommand.Execute(null);
        }

        private void GetDepartmentsListCommandExecute(object obj)
        {
            try
            {
                var actionResult = GetDepartmentsListActionHandler.ExecuteHandler(new GetDepartmentsListRequestModel
                {
                    SearchParameter = SearchParameter
                });

                Application.Current.Dispatcher.Invoke(() =>
                {
                    DepartmentsList.Clear();
                    int listCounter = 1;
                    foreach (var department in actionResult.Departments)
                    {
                        DepartmentsList.Add(new DepartmentObservableModel()
                        {
                            Number = listCounter,
                            Title = department.Title,
                            WorkerDuties = department.WorkerDuties,
                            DepartmentId = department.DepartmentId,
                            WorkersCount = department.WorkersCount.ToString(),
                            GetDepartmentFullInfoCommand = new RelayCommand(GetFullDepartmentInfoCommandExecute)
                        });
                    }
                });
            }
            catch { /**/ }
        }

        private void UpdateDepartmentCommandExecute(object obj)
        {
            try
            {
                UpdateDepartmentActionHandler.ExecuteHandler(new UpdateDepartmentRequestModel()
                {
                    DepartmentId = UpdatingDepartmentId,
                    Title = UpdatingTitle,
                    WorkerDuties = UpdatingWorkersDuties
                });

                GetDepartmentsListCommand.Execute(null);
            }
            catch { /**/ }
        }

        private void CreateDepartmentCommandExecute(object obj)
        {
            try
            {
                CreateDepartmentActionHandler.ExecuteHandler(new CreateDepartmentsRequestModel
                {
                    Title = CreatingTitle,
                    WorkerDuties = CreatingWorkersDuties
                });

                GetDepartmentsListCommand.Execute(null);
            }
            catch { /**/ }
        }

        private void RemoveDepartmentCommandExecute(object obj)
        {
            try
            {
                RemoveDepartmentActionHandler.ExecuteHandler(new DeleteDepartmentRequestModel
                {
                    DepartmentId = UpdatingDepartmentId
                });

                GetDepartmentsListCommand.Execute(null);
            }
            catch { /**/ }
        }

        private void GetFullDepartmentInfoCommandExecute(object obj)
        {
            try
            {
                int departmentId = (int)obj;
                var actionResult = GetFullDepartmentInfoActionHandler.ExecuteHandler(new GetFullDepartmentInfoRequestModel
                {
                    DepartmentId = departmentId
                });

                UpdatingTitle = actionResult.Title;
                UpdatingWorkersDuties = actionResult.WorkerDuties;
                UpdatingDepartmentId = actionResult.DepartmentId;
                RemoveDepartmentButtonVisibility = actionResult.Workers.Any() ? Visibility.Hidden : Visibility.Visible;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    DepartmentWorkers.Clear();
                    int listCounter = 1;
                    foreach (var worker in actionResult.Workers)
                    {
                        DepartmentWorkers.Add(new WorkerObservableModel()
                        {
                            Number = listCounter,
                            Name = worker.Name,
                            Salary = worker.Salary,
                            HireDate = worker.HireDate.ToString("dd.MM.yyyy")
                        });
                    }
                });
            }
            catch { /**/ }
        }
    }
}