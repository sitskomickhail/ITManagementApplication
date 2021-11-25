using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Handlers.Projects;
using ITManagementClient.Handlers.ProjectWorkers;
using ITManagementClient.Infrastructure;
using ITManagementClient.Models.Common.ObservableModels;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Projects;
using ITManagementClient.Models.RequestModels.ProjectWorkers;
using ITManagementClient.Models.ResponseModels.Projects;
using ITManagementClient.Models.ResponseModels.ProjectWorkers;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;

namespace ITManagementClient.ViewModels.ResourceManager
{
    public class ResourceProjectsListViewModel : BaseViewModel, IControlViewModel
    {
        public IEnumerable<PageDefinition> PageDefinitions => new List<PageDefinition> { PageDefinition.ResourceManager };

        public ObservableCollection<ProjectObservableModel> ProjectsList { get; set; }
        public ObservableCollection<ProjectWorkerObservableModel> ProjectWorkersList { get; set; }
        public ObservableCollection<AppendingProjectWorkerObservableModel> AppendingWorkersList { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        private string _projectDescription;
        public string ProjectDescription
        {
            get => _projectDescription;
            set { _projectDescription = value; OnPropertyChanged(nameof(ProjectDescription)); }
        }

        private string _technologiesStack;
        public string TechnologiesStack
        {
            get => _technologiesStack;
            set { _technologiesStack = value; OnPropertyChanged(nameof(TechnologiesStack)); }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; OnPropertyChanged(nameof(IsActive)); }
        }

        private string _searchParameter;
        public string SearchParameter
        {
            get => _searchParameter;
            set { _searchParameter = value; OnPropertyChanged(nameof(SearchParameter)); }
        }

        private int _editingProjectId;
        public int EditingProjectId
        {
            get => _editingProjectId;
            set { _editingProjectId = value; OnPropertyChanged(nameof(EditingProjectId)); }
        }


        private int _editingWorkerId;
        public int EditingWorkerId
        {
            get => _editingWorkerId;
            set { _editingWorkerId = value; OnPropertyChanged(nameof(EditingWorkerId)); }
        }

        private string _updatingWorkerCost;
        public string UpdatingWorkerCost
        {
            get => _updatingWorkerCost;
            set
            {
                if (Double.TryParse(value, out _) && Double.Parse(value) >= 0)
                {
                    _updatingWorkerCost = value;
                }

                OnPropertyChanged(nameof(UpdatingWorkerCost));
            }
        }

        public ICommand SearchByParameterCommand { get; set; }
        public ICommand UpdateProjectCommand { get; set; }
        public ICommand UpdateWorkerCostCommand { get; set; }

        public BaseActionHandler<GetProjectByIdRequestModel, GetProjectByIdResponseModel> GetProjectByIdActionHandler { get; set; }
        public BaseActionHandler<SearchProjectsRequestModel, SearchProjectResponseModel> SearchProjectsActionHandler { get; set; }
        public BaseActionHandler<UpdateProjectRequestModel, UpdateProjectResponseModel> UpdateProjectActionHandler { get; set; }
        public BaseActionHandler<RemoveWorkerFromProjectRequestModel, RemoveWorkerFromProjectResponseModel> RemoveProjectWorkerActionHandler { get; set; }
        public BaseActionHandler<AddWorkerToProjectRequestModel, AddWorkerToProjectResponseModel> AddWorkerProjectActionHandler { get; set; }
        public BaseActionHandler<UpdateWorkerCostOnProjectRequestModel, UpdateWorkerCostOnProjectResponseModel> UpdateWorkerCostActionHandler { get; set; }
        public BaseActionHandler<GetListOfAvailableDevelopersRequestModel, GetListOfAvailableDevelopersResponseModel> GetListOfAvailableDevelopersActionHandler { get; set; }

        public ResourceProjectsListViewModel()
        {
            ProjectsList = new ObservableCollection<ProjectObservableModel>();
            ProjectsList.Add(new ProjectObservableModel());
            ProjectWorkersList = new ObservableCollection<ProjectWorkerObservableModel>();
            ProjectWorkersList.Add(new ProjectWorkerObservableModel());
            AppendingWorkersList = new ObservableCollection<AppendingProjectWorkerObservableModel>();
            AppendingWorkersList.Add(new AppendingProjectWorkerObservableModel());

            SearchParameter = String.Empty;

            SearchProjectsActionHandler = new SearchProjectsActionHandler();
            GetProjectByIdActionHandler = new GetProjectInfoActionHandler();
            UpdateProjectActionHandler = new UpdateProjectActionHandler();
            RemoveProjectWorkerActionHandler = new RemoveProjectWorkerActionHandler();
            AddWorkerProjectActionHandler = new AddWorkerProjectActionHandler();
            UpdateWorkerCostActionHandler = new UpdateWorkerCostActionHandler();
            GetListOfAvailableDevelopersActionHandler = new GetListOfAvailableDevelopersActionHandler();

            SearchByParameterCommand = new RelayCommand(SearchByParameterCommandExecute);
            UpdateProjectCommand = new RelayCommand(UpdateProjectCommandExecute);
            UpdateWorkerCostCommand = new RelayCommand(UpdateWorkerCostCommandExecute);
        }

        public void LoadInstance()
        {
            SearchByParameterCommand.Execute(null);
        }

        private void SearchByParameterCommandExecute(object obj)
        {
            try
            {
                var actionResult = SearchProjectsActionHandler.ExecuteHandler(new SearchProjectsRequestModel
                {
                    SearchParameter = SearchParameter
                });

                Application.Current.Dispatcher.Invoke(() =>
                {
                    ProjectsList.Clear();
                    int listCounter = 1;
                    foreach (var project in actionResult.Projects)
                    {
                        ProjectsList.Add(new ProjectObservableModel()
                        {
                            Number = listCounter++,
                            ProjectId = project.ProjectId,
                            Title = project.Title,
                            StartDate = project.StartDate.ToString("dd.MM.yyyy"),
                            Active = project.Active ? "Да" : "Нет",
                            TechnologiesStack = project.TechnologiesStack,
                            ShowProjectDetailsCommand = new RelayCommand(GetProjectByIdCommandExecute),
                            ShowExistingDevelopers = new RelayCommand(GetListOfAvailableDevelopersCommandExecute)
                        });
                    }
                });
            }
            catch { /**/ }
        }

        private void GetProjectByIdCommandExecute(object obj)
        {
            try
            {
                var projectId = (int)obj;
                var actionResult = GetProjectByIdActionHandler.ExecuteHandler(new GetProjectByIdRequestModel
                {
                    ProjectId = projectId
                });

                Application.Current.Dispatcher.Invoke(() =>
                {
                    ProjectWorkersList.Clear();
                    int listCounter = 1;

                    foreach (var worker in actionResult.Workers)
                    {
                        ProjectWorkersList.Add(new ProjectWorkerObservableModel()
                        {
                            Position = listCounter++,
                            Name = worker.Name,
                            Cost = worker.Cost,
                            Department = worker.Department,
                            WorkerId = worker.WorkerId,
                            RemoveWorkerFromProjectCommand = new RelayCommand(RemoveWorkerFromProjectCommandExecute),
                            GetWorkerCostOnProjectCommand = new RelayCommand(GetWorkerCostOnProjectCommandExecute)
                        });
                    }
                });

                ProjectDescription = actionResult.Description;
                TechnologiesStack = actionResult.TechnologiesStack;
                Title = actionResult.Title;
                IsActive = actionResult.IsActive;
                StartDate = actionResult.StartDate;
                EditingProjectId = projectId;
            }
            catch { /**/ }
        }

        private void UpdateProjectCommandExecute(object obj)
        {
            try
            {
                UpdateProjectActionHandler.ExecuteHandler(new UpdateProjectRequestModel
                {
                    ProjectId = EditingProjectId,
                    TechnologiesStack = TechnologiesStack,
                    Title = Title,
                    Description = ProjectDescription,
                    StartDate = StartDate,
                    IsActive = IsActive
                });
            }
            catch { /**/ }
        }

        private void RemoveWorkerFromProjectCommandExecute(object obj)
        {
            try
            {
                var workerId = (int)obj;
                RemoveProjectWorkerActionHandler.ExecuteHandler(new RemoveWorkerFromProjectRequestModel
                {
                    ProjectId = EditingProjectId,
                    WorkerId = workerId
                });
            }
            catch { /**/ }
        }

        private void GetListOfAvailableDevelopersCommandExecute(object obj)
        {
            try
            {
                int projectId = (int)obj;
                var actionResult = GetListOfAvailableDevelopersActionHandler.ExecuteHandler(
                    new GetListOfAvailableDevelopersRequestModel
                    {
                        ProjectId = projectId
                    });

                Application.Current.Dispatcher.Invoke(() =>
                {
                    AppendingWorkersList.Clear();
                    int listCounter = 1;

                    foreach (var worker in actionResult.AvailableDevelopers)
                    {
                        AppendingWorkersList.Add(new AppendingProjectWorkerObservableModel()
                        {
                            Number = listCounter++,
                            Name = worker.Name,
                            Salary = worker.Salary.ToString(CultureInfo.InvariantCulture),
                            Department = worker.Department,
                            WorkerId = worker.WorkerId,
                            AddWorkerToProjectCommand = new RelayCommand(AddWorkerToProjectCommandExecute)
                        });
                    }
                });

                EditingProjectId = projectId;
            }
            catch { /**/ }
        }

        private void AddWorkerToProjectCommandExecute(object obj)
        {
            try
            {
                var workerId = (int)obj;
                AddWorkerProjectActionHandler.ExecuteHandler(new AddWorkerToProjectRequestModel()
                {
                    ProjectId = EditingProjectId,
                    WorkerId = workerId
                });
            }
            catch { /**/ }
        }

        private void UpdateWorkerCostCommandExecute(object obj)
        {
            try
            {
                UpdateWorkerCostActionHandler.ExecuteHandler(new UpdateWorkerCostOnProjectRequestModel
                {
                    ProjectId = EditingProjectId,
                    WorkerId = EditingWorkerId,
                    Cost = Double.Parse(UpdatingWorkerCost)
                });
            }
            catch { /**/ }
        }

        private void GetWorkerCostOnProjectCommandExecute(object obj)
        {
            try
            {
                var workerId = (int)obj;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    UpdatingWorkerCost = ProjectWorkersList.FirstOrDefault(p => p.WorkerId == workerId)?.Cost
                        .ToString();
                });

                EditingWorkerId = workerId;
            }
            catch { /**/ }
        }
    }
}