package handlers;

import com.google.gson.Gson;
import constants.ExecutionResults;
import handlers.base.BaseRequestHandler;
import handlers.interfaces.IRequestHandlerProvider;
import models.transferModels.TransferRequestModel;
import models.transferModels.TransferResponseModel;
import models.transferModels.responses.ErrorTransferResponseModel;

import java.util.ArrayList;
import java.util.List;

public class RequestHandlerProvider implements IRequestHandlerProvider {

    private List<BaseRequestHandler> requestHandlers;

    public RequestHandlerProvider() {
        requestHandlers = new ArrayList<>();
    }

    public TransferResponseModel Execute(TransferRequestModel requestModel) {
        var responseModel = new TransferResponseModel();
        var errorModel = new ErrorTransferResponseModel();
        errorModel.errorMessage = "Handler not found";

        responseModel.executionCode = ExecutionResults.ERROR_CODE;
        responseModel.executionResult = new Gson().toJson(errorModel);

        for (var handler : requestHandlers) {
            if (handler.GetHandlerCode() == requestModel.ActionCode) {
                responseModel = handler.HandleRequest(requestModel.ActionModel);
            }
        }

        return responseModel;
    }
}