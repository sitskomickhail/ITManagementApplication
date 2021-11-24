using System.Collections.Generic;
using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.RequestModels.Requests
{
    public class FilterRequestsListRequestModel
    {
        public IEnumerable<RequestType> FilteringTypes { get; set; }
    }
}