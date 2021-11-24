using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Requests;
using ITManagementClient.Models.ResponseModels.Requests;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Requests
{
    public class GetUserRequestsHistoryActionHandler : BaseActionHandler<GetUserRequestsHistoryRequestModel, GetUserRequestsHistoryResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.GET_USER_REQUESTS_HISTORY;

        protected override TransferResponseModel HandleResult(GetUserRequestsHistoryRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}