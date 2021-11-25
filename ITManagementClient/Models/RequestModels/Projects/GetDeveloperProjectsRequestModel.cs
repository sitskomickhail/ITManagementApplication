using System;

namespace ITManagementClient.Models.RequestModels.Projects
{
    public class GetDeveloperProjectsRequestModel
    {
        public string SearchParameter { get; set; } = String.Empty;

        public int DeveloperId { get; set; }
    }
}