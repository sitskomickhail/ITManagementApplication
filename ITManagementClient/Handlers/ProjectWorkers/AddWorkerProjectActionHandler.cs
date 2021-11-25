using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.ProjectWorkers;
using ITManagementClient.Models.ResponseModels.ProjectWorkers;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.ProjectWorkers
{
    public class AddWorkerProjectActionHandler : BaseActionHandler<AddWorkerToProjectRequestModel, AddWorkerToProjectResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.ADD_WORKER_TO_PROJECT;

        protected override TransferResponseModel HandleResult(AddWorkerToProjectRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}