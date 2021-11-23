using System;
using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.ResponseModels.Requests
{
    public class MyRequestHistoryModel
    {
        public int RequestId { get; set; }

        public RequestType RequestType { get; set; }

        public DateTime CreateTime { get; set; }

        public string RequestDescription { get; set; }

        public RequestStatus RequestStatus { get; set; }
    }
}