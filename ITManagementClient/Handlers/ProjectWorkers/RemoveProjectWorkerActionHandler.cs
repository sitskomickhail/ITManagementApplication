using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.ProjectWorkers;
using ITManagementClient.Models.ResponseModels.ProjectWorkers;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.ProjectWorkers
{
    public class RemoveProjectWorkerActionHandler : BaseActionHandler<RemoveWorkerFromProjectRequestModel, RemoveWorkerFromProjectResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.REMOVE_WORKER_FROM_PROJECT;

        protected override TransferResponseModel HandleResult(RemoveWorkerFromProjectRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}