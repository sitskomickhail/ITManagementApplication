using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Projects;
using ITManagementClient.Models.ResponseModels.Projects;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Projects
{
    public class UpdateProjectActionHandler : BaseActionHandler<UpdateProjectRequestModel, UpdateProjectResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.UPDATE_PROJECT;

        protected override TransferResponseModel HandleResult(UpdateProjectRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}