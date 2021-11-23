using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Requests;
using ITManagementClient.Models.ResponseModels.Requests;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Requests
{
    public class CreateRequestActionHandler : BaseActionHandler<CreateRequestRequestModel, CreateRequestResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.CREATE_REQUEST;

        protected override TransferResponseModel HandleResult(CreateRequestRequestModel model)
        {
            var requestModel = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(requestModel);

            return HandlerManager.GetTcpServiceInstance().ReadStream();
        }
    }
}