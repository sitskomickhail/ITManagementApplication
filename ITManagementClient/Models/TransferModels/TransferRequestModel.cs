using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.TransferModels
{
    public class TransferRequestModel
    {
        public HandlerCodes ActionCode { get; set; }
        public string ActionModel { get; set; }
    }
}