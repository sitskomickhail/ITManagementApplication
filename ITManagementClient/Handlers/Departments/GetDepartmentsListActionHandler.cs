﻿using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Departments;
using ITManagementClient.Models.ResponseModels.Departments;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Departments
{
    public class GetDepartmentsListActionHandler : BaseActionHandler<GetDepartmentsListRequestModel, GetDepartmentsListResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.GET_DEPARTMENTS_LIST;

        protected override TransferResponseModel HandleResult(GetDepartmentsListRequestModel model)
        {
            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}