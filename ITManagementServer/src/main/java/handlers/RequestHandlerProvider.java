package handlers;

import com.google.gson.Gson;
import constants.ExecutionResults;
import handlers.base.BaseRequestHandler;
import handlers.implementation.projects.*;
import handlers.implementation.workers.*;
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
        requestHandlers.add(new RegisterWorkerRequestHandler());
        requestHandlers.add(new LoginWorkerRequestHandler());
        requestHandlers.add(new GetWorkersListRequestHandler());
        requestHandlers.add(new UpdateWorkerRequestHandler());
        requestHandlers.add(new GetWorkerByIdRequestHandler());
        requestHandlers.add(new CreateWorkerRequestHandler());
        requestHandlers.add(new SearchProjectsRequestHandler());
        requestHandlers.add(new GetProjectByIdRequestHandler());
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