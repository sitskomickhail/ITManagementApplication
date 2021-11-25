using System.Windows.Input;

namespace ITManagementClient.Models.Common.ObservableModels
{
    public class AppendingProjectWorkerObservableModel
    {
        public int Number { get; set; }

        public int WorkerId { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public string Salary { get; set; }

        public ICommand AddWorkerToProjectCommand { get; set; }
    }
}