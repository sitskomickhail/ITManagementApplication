using System.Windows;
using System.Windows.Input;

namespace ITManagementClient.Models.Common.ObservableModels
{
    public class WorkerObservableModel
    {
        public ICommand ShowWorkerDetailsCommand { get; set; }
        public int WorkerId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public string HireDate { get; set; }
        public Visibility SalaryColumnVisibility { get; set; }
    }
}