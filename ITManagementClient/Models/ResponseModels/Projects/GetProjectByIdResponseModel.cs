using System.Collections.Generic;

namespace ITManagementClient.Models.ResponseModels.Projects
{
    public class GetProjectByIdResponseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string TechnologiesStack { get; set; }

        public List<ProjectWorkerResponseModel> Workers { get; set; }
    }
}