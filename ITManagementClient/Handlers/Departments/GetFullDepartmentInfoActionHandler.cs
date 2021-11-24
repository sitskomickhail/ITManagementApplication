using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Departments;
using ITManagementClient.Models.ResponseModels.Departments;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Departments
{
    public class GetFullDepartmentInfoActionHandler : BaseActionHandler<GetFullDepartmentInfoRequestModel, GetFullDepartmentInfoResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.GET_DEPARTMENT_FULL_INFO;

        protected override TransferResponseModel HandleResult(GetFullDepartmentInfoRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}