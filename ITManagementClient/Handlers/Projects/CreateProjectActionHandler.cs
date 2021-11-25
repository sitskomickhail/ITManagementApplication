using System;
using ITManagementClient.Exceptions;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Projects;
using ITManagementClient.Models.ResponseModels.Projects;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Projects
{
    public class CreateProjectActionHandler : BaseActionHandler<CreateProjectRequestModel, CreateProjectResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.CREATE_PROJECT;

        protected override TransferResponseModel HandleResult(CreateProjectRequestModel model)
        {
            if (String.IsNullOrEmpty(model.Description) || String.IsNullOrEmpty(model.TechnologiesStack)
                                                        || String.IsNullOrEmpty(model.Title))
            {
                throw new HandlerExecutionException("Заполните все обязательные поля");
            }

            var request = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(request);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}