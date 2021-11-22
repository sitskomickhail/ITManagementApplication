using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Projects;
using ITManagementClient.Models.ResponseModels.Projects;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Projects
{
    public class GetProjectInfoActionHandler : BaseActionHandler<GetProjectByIdRequestModel, GetProjectByIdResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.GET_PROJECT_BY_ID;

        protected override TransferResponseModel HandleResult(GetProjectByIdRequestModel model)
        {
            var requestModel = CreateRequestModel(model);
            HandlerManager.GetTcpServiceInstance().WriteStream(requestModel);

            return HandlerManager.GetTcpServiceInstance().ReadStream();
        }
    }
}