using System.Windows.Input;

namespace ITManagementClient.Models.Common.ObservableModels
{
    public class ProjectWorkerObservableModel
    {
        public int WorkerId { get; set; }

        public int Position { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public decimal Cost { get; set; }

        public ICommand RemoveWorkerFromProjectCommand { get; set; }
        public ICommand GetWorkerCostOnProjectCommand { get; set; }
    }
}