using System;
using ITManagementClient.Exceptions;
using ITManagementClient.Managers;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.TransferModels;
using ITManagementClient.Models.TransferModels.ResponseModels;
using ITManagementClient.Navigation;
using Newtonsoft.Json;

namespace ITManagementClient.Handlers.Base
{
    public abstract class BaseActionHandler<TIncomingModel, TOutgoingModel> where TIncomingModel : class where TOutgoingModel : class
    {
        protected TcpHandlerManager HandlerManager { get; set; }

        public abstract HandlerCodes HandlerCode { get; }

        protected BaseActionHandler()
        {
            HandlerManager = TcpHandlerManager.GetTcpHandlerManager();
        }

        protected abstract TransferResponseModel HandleResult(TIncomingModel model);

        public TOutgoingModel ExecuteHandler(TIncomingModel model)
        {
            TOutgoingModel outgoingModel = null;
            try
            {
                var result = HandleResult(model);

                if (result.ExecutionCode == ExecutionCode.ERROR_CODE)
                {
                    throw new HandlerExecutionException(!String.IsNullOrEmpty(result.ExecutionResult) ? result.ExecutionResult : $"Exception in handler [{HandlerCode}]");
                }

                var successResultModel = JsonConvert.DeserializeObject<SuccessTransferResponseModel<TOutgoingModel>>(result.ExecutionResult);
                outgoingModel = successResultModel.ResponseModel;
            }
            catch (HandlerExecutionException handlerException)
            {
                Mediator.Notify("SnackbarMessageShow", handlerException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return outgoingModel;
        }

        protected virtual TransferRequestModel CreateRequestModel(TIncomingModel model)
        {
            return new TransferRequestModel
            {
                ActionCode = HandlerCode,
                ActionModel = JsonConvert.SerializeObject(model)
            };
        }
    }
}