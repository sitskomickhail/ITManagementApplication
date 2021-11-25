namespace ITManagementClient.Models.ResponseModels.Departments
{
    public class DepartmentGridResponseModel
    {
        public int DepartmentId { get; set; }

        public string Title { get; set; }

        public string WorkerDuties { get; set; }

        public int WorkersCount { get; set; }
    }
}