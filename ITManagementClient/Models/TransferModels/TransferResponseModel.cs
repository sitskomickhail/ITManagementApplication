using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.TransferModels
{
    public class TransferResponseModel
    {
        public ExecutionCode ExecutionCode { get; set; }
        public string ExecutionResult { get; set; }
    }
}