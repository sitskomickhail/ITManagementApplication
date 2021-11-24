using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Workers;
using ITManagementClient.Models.ResponseModels.Workers;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Workers
{
    public class GetWorkerByIdActionHandler : BaseActionHandler<GetWorkerByIdRequestModel, GetWorkerByIdResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.GET_WORKER_BY_ID;

        protected override TransferResponseModel HandleResult(GetWorkerByIdRequestModel model)
        {
            var requestModel = CreateRequestModel(model);
            HandlerManager.GetTcpServiceInstance().WriteStream(requestModel);

            return HandlerManager.GetTcpServiceInstance().ReadStream();
        }
    }
}