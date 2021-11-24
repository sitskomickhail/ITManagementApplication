namespace ITManagementClient.Models.RequestModels.Departments
{
    public class UpdateDepartmentRequestModel
    {
        public int DepartmentId { get; set; }

        public string Title { get; set; }

        public string WorkerDuties { get; set; }
    }
}