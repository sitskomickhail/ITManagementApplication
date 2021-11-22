using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Projects;
using ITManagementClient.Models.ResponseModels.Projects;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Projects
{
    public class SearchProjectsActionHandler : BaseActionHandler<SearchProjectsRequestModel, SearchProjectResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.GET_PROJECTS_LIST;

        protected override TransferResponseModel HandleResult(SearchProjectsRequestModel model)
        {
            var requestModel = CreateRequestModel(model);
            HandlerManager.GetTcpServiceInstance().WriteStream(requestModel);

            return HandlerManager.GetTcpServiceInstance().ReadStream();
        }
    }
}