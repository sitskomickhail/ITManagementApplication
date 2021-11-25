using System;

namespace ITManagementClient.Models.RequestModels.Projects
{
    public class UpdateProjectRequestModel
    {
        public int ProjectId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string TechnologiesStack { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsActive { get; set; }
    }
}