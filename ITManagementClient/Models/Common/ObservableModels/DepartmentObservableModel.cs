using System.Windows.Input;

namespace ITManagementClient.Models.Common.ObservableModels
{
    public class DepartmentObservableModel
    {
        public int Number { get; set; }

        public int DepartmentId { get; set; }

        public string Title { get; set; }

        public string WorkerDuties { get; set; }

        public string WorkersCount { get; set; }

        public ICommand GetDepartmentFullInfoCommand { get; set; }
    }
}