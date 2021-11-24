using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.RequestModels.Requests
{
    public class ChangeRequestResolvingStatusRequestModel
    {
        public int RequestId { get; set; }

        public RequestStatus RequestStatus { get; set; }
    }
}