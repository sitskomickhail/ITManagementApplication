using System.Windows.Input;

namespace ITManagementClient.Models.Common.ObservableModels
{
    public class ProjectObservableModel
    {
        public ICommand ShowProjectDetailsCommand { get; set; }
        public int Number { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string TechnologiesStack { get; set; }
        public string StartDate { get; set; }
        public string Active { get; set; }
        public ICommand ShowExistingDevelopers { get; set; }
    }
}