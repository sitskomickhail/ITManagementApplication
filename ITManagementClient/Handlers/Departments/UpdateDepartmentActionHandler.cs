using System;
using ITManagementClient.Exceptions;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Departments;
using ITManagementClient.Models.ResponseModels.Departments;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Departments
{
    public class UpdateDepartmentActionHandler : BaseActionHandler<UpdateDepartmentRequestModel, UpdateDepartmentResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.UPDATE_DEPARTMENT;

        protected override TransferResponseModel HandleResult(UpdateDepartmentRequestModel model)
        {
            if (String.IsNullOrWhiteSpace(model.Title) || String.IsNullOrEmpty(model.WorkerDuties))
            {
                throw new HandlerExecutionException("Все обязательные поля должны быть заполнены");
            }

            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}