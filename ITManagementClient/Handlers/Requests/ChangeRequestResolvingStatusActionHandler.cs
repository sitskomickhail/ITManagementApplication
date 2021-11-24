using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Requests;
using ITManagementClient.Models.ResponseModels.Requests;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Requests
{
    public class ChangeRequestResolvingStatusActionHandler : BaseActionHandler<ChangeRequestResolvingStatusRequestModel, ChangeRequestResolvingStatusResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.CHANGE_REQUEST_STATUS;

        protected override TransferResponseModel HandleResult(ChangeRequestResolvingStatusRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}