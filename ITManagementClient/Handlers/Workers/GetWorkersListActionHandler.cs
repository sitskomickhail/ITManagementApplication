using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Workers;
using ITManagementClient.Models.ResponseModels.Workers;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Workers
{
    public class GetWorkersListActionHandler : BaseActionHandler<GetWorkerListRequestModel, GetWorkerListResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.GET_WORKERS_LIST;

        protected override TransferResponseModel HandleResult(GetWorkerListRequestModel model)
        {
            var requestModel = CreateRequestModel(model);
            HandlerManager.GetTcpServiceInstance().WriteStream(requestModel);

            return HandlerManager.GetTcpServiceInstance().ReadStream();
        }
    }
}