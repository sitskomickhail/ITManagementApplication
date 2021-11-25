using System;
using System.Collections.Generic;
using System.Windows.Input;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Handlers.Projects;
using ITManagementClient.Infrastructure;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Projects;
using ITManagementClient.Models.ResponseModels.Projects;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;
using ITManagementClient.ViewModels.ResourceManager;

namespace ITManagementClient.ViewModels.Projects
{
    public class ProjectCreationViewModel : BaseViewModel, IControlViewModel
    {
        public IEnumerable<PageDefinition> PageDefinitions => new List<PageDefinition> { PageDefinition.ResourceManager };

        private string _projectTitle;
        public string ProjectTitle
        {
            get => _projectTitle;
            set { _projectTitle = value; OnPropertyChanged(nameof(ProjectTitle)); }
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

        public ICommand CreateProjectCommand { get; set; }

        public BaseActionHandler<CreateProjectRequestModel, CreateProjectResponseModel> CreateProjectActionHandler { get; set; }

        public ProjectCreationViewModel()
        {
            StartDate = DateTime.Now;

            CreateProjectActionHandler = new CreateProjectActionHandler();

            CreateProjectCommand = new RelayCommand(CreateProjectCommandExecute);
        }

        public void LoadInstance() { }

        private void CreateProjectCommandExecute(object obj)
        {
            try
            {
                CreateProjectActionHandler.ExecuteHandler(new CreateProjectRequestModel
                {
                    Description = ProjectDescription,
                    StartDate = StartDate,
                    TechnologiesStack = TechnologiesStack,
                    Title = ProjectTitle
                });

                Mediator.Notify(nameof(ResourceProjectsListViewModel), nameof(ResourceProjectsListViewModel));
            }
            catch { /**/ }
        }
    }
}