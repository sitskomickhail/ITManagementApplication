using System.Collections.Generic;

namespace ITManagementClient.Models.ResponseModels.Requests
{
    public class GetUserRequestsHistoryResponseModel
    {
        public List<MyRequestHistoryModel> RequestsList { get; set; }
    }
}