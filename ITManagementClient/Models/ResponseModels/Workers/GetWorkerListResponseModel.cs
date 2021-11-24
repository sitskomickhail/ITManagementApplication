using System.Collections.Generic;

namespace ITManagementClient.Models.ResponseModels.Workers
{
    public class GetWorkerListResponseModel
    {
        public List<WorkerGridResponseModel> WorkersList { get; set; }
    }
}