using System;
using ITManagementClient.Exceptions;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Workers;
using ITManagementClient.Models.ResponseModels.Workers;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Workers
{
    public class LoginWorkerActionHandler : BaseActionHandler<LoginRequestModel, LoginResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.LOGIN_WORKER;
        
        protected override TransferResponseModel HandleResult(LoginRequestModel model)
        {
            if (String.IsNullOrEmpty(model.Login) || String.IsNullOrEmpty(model.Password))
            {
                throw new HandlerExecutionException("Поля логин и пароль должны быть заполнены");
            }

            var requestModel = CreateRequestModel(model);
            HandlerManager.GetTcpServiceInstance().WriteStream(requestModel);

            return HandlerManager.GetTcpServiceInstance().ReadStream();
        }
    }
}