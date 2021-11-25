using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Connectors;
using ITManagementClient.Models.ResponseModels.Connectors;
using ITManagementClient.Models.TransferModels;
using Newtonsoft.Json;

namespace ITManagementClient.Handlers.Connectors
{
    public class CloseConnectionActionHandler : BaseActionHandler<CloseConnectionRequestModel, CloseConnectionResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.CLOSE_CONNECTION;

        protected override TransferResponseModel HandleResult(CloseConnectionRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);

            return new TransferResponseModel
            {
                ExecutionCode = ExecutionCode.SUCCESS_CODE,
                ExecutionResult = JsonConvert.SerializeObject(new CloseConnectionResponseModel())
            };
        }
    }
}