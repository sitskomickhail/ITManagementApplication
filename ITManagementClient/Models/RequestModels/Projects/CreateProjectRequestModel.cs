using System;

namespace ITManagementClient.Models.RequestModels.Projects
{
    public class CreateProjectRequestModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string TechnologiesStack { get; set; }

        public DateTime StartDate { get; set; }
    }
}