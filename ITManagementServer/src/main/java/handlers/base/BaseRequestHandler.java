package handlers.base;

import com.google.gson.Gson;
import constants.ExecutionResults;
import models.transferModels.TransferResponseModel;

import java.lang.reflect.Type;

public abstract class BaseRequestHandler<ModelIn, ModelOut> {

    public TransferResponseModel HandleRequest(String jsonModel) {
        var responseModel = new TransferResponseModel();

        try {
            ModelIn incomingModel = new Gson().fromJson(jsonModel, getIncomingModelType());

            var resultModel = Execute(incomingModel);
            responseModel.executionResult = new Gson().toJson(resultModel);
            responseModel.executionCode = ExecutionResults.SUCCESS_CODE;
        } catch (Exception ex) {
            responseModel.executionCode = ExecutionResults.ERROR_CODE;
            responseModel.executionResult = ex.getMessage();
        }

        return responseModel;
    }

    public abstract int GetHandlerCode();

    public abstract Type getIncomingModelType();

    protected abstract ModelOut Execute(ModelIn model) throws Exception;
}