using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Projects;
using ITManagementClient.Models.ResponseModels.Projects;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Projects
{
    public class GetDeveloperProjectsActionHandler : BaseActionHandler<GetDeveloperProjectsRequestModel, SearchProjectResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.GET_DEVELOPER_PROJECTS;

        protected override TransferResponseModel HandleResult(GetDeveloperProjectsRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}