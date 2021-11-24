using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Requests;
using ITManagementClient.Models.ResponseModels.Requests;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Requests
{
    public class UpdateRequestActionHandler : BaseActionHandler<UpdateRequestRequestModel, UpdateRequestResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.UPDATE_REQUEST;

        protected override TransferResponseModel HandleResult(UpdateRequestRequestModel model)
        {
            var request = CreateRequestModel(model);
            
            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}