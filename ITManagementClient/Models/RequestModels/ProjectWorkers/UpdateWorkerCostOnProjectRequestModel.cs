namespace ITManagementClient.Models.RequestModels.ProjectWorkers
{
    public class UpdateWorkerCostOnProjectRequestModel
    {
        public int ProjectId { get; set; }

        public int WorkerId { get; set; }

        public double Cost { get; set; }
    }
}