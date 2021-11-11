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


        private void ShowWorkerDetailsCommandExecute(object obj)
        {

        }


        public WorkersListViewModel()
        {
            WorkersList = new ObservableCollection<WorkerObservableModel>();

            //ShowWorkerDetailsCommand = new RelayCommand(ShowWorkerDetailsCommandExecute);

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
        }
    }
}