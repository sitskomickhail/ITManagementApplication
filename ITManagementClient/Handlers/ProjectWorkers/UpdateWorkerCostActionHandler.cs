using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.ProjectWorkers;
using ITManagementClient.Models.ResponseModels.ProjectWorkers;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.ProjectWorkers
{
    public class UpdateWorkerCostActionHandler : BaseActionHandler<UpdateWorkerCostOnProjectRequestModel, UpdateWorkerCostOnProjectResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.UPDATE_WORKER_PROJECT_COST;

        protected override TransferResponseModel HandleResult(UpdateWorkerCostOnProjectRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}