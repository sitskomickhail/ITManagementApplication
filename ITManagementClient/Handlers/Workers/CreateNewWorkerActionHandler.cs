using System;
using ITManagementClient.Exceptions;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Workers;
using ITManagementClient.Models.ResponseModels.Workers;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Workers
{
    public class CreateNewWorkerActionHandler : BaseActionHandler<CreateNewWorkerRequestModel, CreateNewWorkerResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.CREATE_WORKER;

        protected override TransferResponseModel HandleResult(CreateNewWorkerRequestModel model)
        {
            if(String.IsNullOrEmpty(model.Password) ||
               String.IsNullOrEmpty(model.Login) ||
               String.IsNullOrEmpty(model.Name))
            {
                throw new HandlerExecutionException("Все обязательные поля должны быть заполнены!");
            }

            if (model.Password.Length < 8)
            {
                throw new HandlerExecutionException("Длина пароля должна быть не меньше 8!");
            }

            if (model.BirthDate < new DateTime(1900, 1, 1))
            {
                throw new HandlerExecutionException("Дата рождения должна быть не меньше 01.01.1900!");
            }

            var requestModel = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(requestModel);

            return HandlerManager.GetTcpServiceInstance().ReadStream();
        }
    }
}