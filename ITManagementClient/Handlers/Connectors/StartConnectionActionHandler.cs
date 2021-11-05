using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Connectors;
using ITManagementClient.Models.ResponseModels.Connectors;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Connectors
{
    public class StartConnectionActionHandler : BaseActionHandler<StartConnectionRequestModel, StartConnectionResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.START_CONNECTION;

        protected override TransferResponseModel HandleResult(StartConnectionRequestModel model)
        {
            var requestModel = CreateRequestModel(model);
            HandlerManager.GetTcpServiceInstance().WriteStream(requestModel);
            
            return HandlerManager.GetTcpServiceInstance().ReadStream();
        }
    }
}