using System;

namespace ITManagementClient.Models.ResponseModels.Projects
{
    public class ProjectGridResponseModel
    {
        public int ProjectId { get; set; }

        public string Title { get; set; }

        public string TechnologiesStack { get; set; }

        public DateTime StartDate { get; set; }

        public bool Active { get; set; }
    }
}