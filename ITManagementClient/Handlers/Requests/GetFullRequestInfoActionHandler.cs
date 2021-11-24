using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Requests;
using ITManagementClient.Models.ResponseModels.Requests;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Requests
{
    public class GetFullRequestInfoActionHandler : BaseActionHandler<GetRequestFullInfoRequestModel, GetRequestFullInfoResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.GET_FULL_REQUEST_INFO;

        protected override TransferResponseModel HandleResult(GetRequestFullInfoRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}