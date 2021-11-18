using System;
using System.Diagnostics;
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
            TOutgoingModel outgoingModel;
            try
            {
                var result = HandleResult(model);

                if (result.ExecutionCode == ExecutionCode.ERROR_CODE)
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorTransferResponseModel>(result.ExecutionResult);
                    throw new HandlerExecutionException(!String.IsNullOrEmpty(errorModel?.ErrorMessage) ? errorModel.ErrorMessage : $"Exception in handler [{HandlerCode}]");
                }

                var successResultModel = JsonConvert.DeserializeObject<SuccessTransferResponseModel<TOutgoingModel>>(result.ExecutionResult);
                outgoingModel = successResultModel.ResponseModel;
            }
            catch (HandlerExecutionException handlerException)
            {
                Mediator.Notify("SnackbarMessageShow", handlerException.Message);
                throw;
            }
            catch (Exception e)
            {
                Mediator.Notify("SnackbarMessageShow", "Exception appeared while making request!");
                Mediator.Notify("SnackbarMessageShow", e.Message);
                Debug.Write(e.Message);
                throw;
            }

            return outgoingModel;
        }

        protected virtual TransferRequestModel CreateRequestModel(TIncomingModel model)
        {
            return new TransferRequestModel
            {
                ActionCode = HandlerCode,
                ActionModel = JsonConvert.SerializeObject(model, new JsonSerializerSettings
                {
                    DateFormatString = "MM.dd.yyyy",
                })
            };
        }
    }
}