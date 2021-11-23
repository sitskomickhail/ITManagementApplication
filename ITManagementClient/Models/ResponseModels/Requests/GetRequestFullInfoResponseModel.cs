using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.ResponseModels.Requests
{
    public class GetRequestFullInfoResponseModel
    {
        public string RequestDescription { get; set; }

        public RequestType RequestType { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public string ResolveNotes { get; set; }
    }
}