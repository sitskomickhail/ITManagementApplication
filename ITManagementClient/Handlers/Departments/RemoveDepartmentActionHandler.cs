using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Departments;
using ITManagementClient.Models.ResponseModels.Departments;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Departments
{
    public class RemoveDepartmentActionHandler : BaseActionHandler<DeleteDepartmentRequestModel, DeleteDepartmentResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.DELETE_DEPARTMENT;

        protected override TransferResponseModel HandleResult(DeleteDepartmentRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;

        }
    }
}