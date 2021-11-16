using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Workers;
using ITManagementClient.Models.ResponseModels.Workers;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Workers
{
    public class UpdateWorkerActionHandler : BaseActionHandler<UpdateWorkerRequestModel, UpdateWorkerResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.UPDATE_WORKER;

        protected override TransferResponseModel HandleResult(UpdateWorkerRequestModel model)
        {
            var requestModel = CreateRequestModel(model);
            HandlerManager.GetTcpServiceInstance().WriteStream(requestModel);

            return HandlerManager.GetTcpServiceInstance().ReadStream();
        }
    }
}