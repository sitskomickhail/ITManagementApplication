using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.RequestModels.Requests
{
    public class UpdateRequestRequestModel
    {
        public int RequestId { get; set; }

        public string ResolveNote { get; set; }

        public RequestStatus RequestStatus { get; set; }
    }
}