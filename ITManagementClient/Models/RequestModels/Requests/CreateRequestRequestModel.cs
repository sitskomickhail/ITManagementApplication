using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.RequestModels.Requests
{
    public class CreateRequestRequestModel
    {
        public int WorkerId { get; set; }

        public string RequestDescription { get; set; }

        public RequestType RequestType { get; set; }
    }
}