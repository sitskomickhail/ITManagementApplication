package handlers.base;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import constants.ExecutionResults;
import models.transferModels.TransferResponseModel;

import java.io.IOException;
import java.lang.reflect.Type;
import java.sql.SQLException;

public abstract class BaseRequestHandler<ModelIn, ModelOut> {

    public TransferResponseModel HandleRequest(String jsonModel) {
        var responseModel = new TransferResponseModel();

        try {
            Type typeToken = new TypeToken<ModelIn>() {
            }.getType();
            ModelIn incomingModel = new Gson().fromJson(jsonModel, typeToken);

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

    protected abstract ModelOut Execute(ModelIn model) throws Exception;
}