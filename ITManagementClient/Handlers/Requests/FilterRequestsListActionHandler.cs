using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Requests;
using ITManagementClient.Models.ResponseModels.Requests;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Requests
{
    public class FilterRequestsListActionHandler : BaseActionHandler<FilterRequestsListRequestModel, FilterRequestsListResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.FILTER_REQUESTS_LIST;

        protected override TransferResponseModel HandleResult(FilterRequestsListRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}