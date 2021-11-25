using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.ProjectWorkers;
using ITManagementClient.Models.ResponseModels.ProjectWorkers;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.ProjectWorkers
{
    public class GetListOfAvailableDevelopersActionHandler : BaseActionHandler<GetListOfAvailableDevelopersRequestModel, GetListOfAvailableDevelopersResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.GET_LIST_OF_AVAILABLE_DEVELOPERS;

        protected override TransferResponseModel HandleResult(GetListOfAvailableDevelopersRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}