using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Handlers.Projects;
using ITManagementClient.Infrastructure;
using ITManagementClient.Models.Common.ObservableModels;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Projects;
using ITManagementClient.Models.ResponseModels.Projects;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;

namespace ITManagementClient.ViewModels.Projects
{
    public class ProjectsListViewModel : BaseViewModel, IControlViewModel
    {
        public IEnumerable<PageDefinition> PageDefinitions => new List<PageDefinition> { PageDefinition.Administrator };

        public ObservableCollection<ProjectObservableModel> ProjectsList { get; set; }
        public ObservableCollection<ProjectWorkerObservableModel> ProjectWorkersList { get; set; }

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

        private string _searchParameter;
        public string SearchParameter
        {
            get => _searchParameter;
            set { _searchParameter = value; OnPropertyChanged(nameof(SearchParameter)); }
        }

        public ICommand SearchByParameterCommand { get; set; }

        public BaseActionHandler<GetProjectByIdRequestModel, GetProjectByIdResponseModel> GetProjectByIdActionHandler { get; set; }
        public BaseActionHandler<SearchProjectsRequestModel, SearchProjectResponseModel> SearchProjectsActionHandler { get; set; }

        public ProjectsListViewModel()
        {
            ProjectsList = new ObservableCollection<ProjectObservableModel>();
            ProjectWorkersList = new ObservableCollection<ProjectWorkerObservableModel>();
            ProjectsList.Add(new ProjectObservableModel());

            SearchParameter = String.Empty;

            SearchProjectsActionHandler = new SearchProjectsActionHandler();
            GetProjectByIdActionHandler = new GetProjectInfoActionHandler();

            SearchByParameterCommand = new RelayCommand(SearchByParameterCommandExecute);
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
                            ShowProjectDetailsCommand = new RelayCommand(GetProjectByIdCommandExecute)
                        });
                    }
                });
            }
            catch
            { /**/ }
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
                            Department = worker.Department
                        });
                    }
                });

                ProjectDescription = actionResult.Description;
                TechnologiesStack = actionResult.TechnologiesStack;
                Title = actionResult.Title;
            }
            catch
            { /**/ }
        }
    }
}