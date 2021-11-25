namespace ITManagementClient.Models.RequestModels.ProjectWorkers 
{
   public class RemoveWorkerFromProjectRequestModel
    {
        public int WorkerId { get; set; }

        public int ProjectId { get; set; }
    }
}