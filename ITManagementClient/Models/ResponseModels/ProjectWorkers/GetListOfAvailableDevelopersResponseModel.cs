using System.Collections.Generic;
using ITManagementClient.Models.ResponseModels.Workers;

namespace ITManagementClient.Models.ResponseModels.ProjectWorkers
{
    public class GetListOfAvailableDevelopersResponseModel
    {
        public IEnumerable<WorkerGridResponseModel> AvailableDevelopers { get; set; }
    }
}