package handlers.base;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import constants.ExecutionResults;
import models.transferModels.TransferResponseModel;
import models.transferModels.responses.ErrorTransferResponseModel;
import models.transferModels.responses.SuccessTransferResponseModel;

import java.lang.reflect.Type;

public abstract class BaseRequestHandler<ModelIn, ModelOut> {

    public TransferResponseModel HandleRequest(String jsonModel) {
        var responseModel = new TransferResponseModel();

        try {
            Gson gson = new GsonBuilder().setDateFormat("MM.dd.yyyy").create();
            ModelIn incomingModel = gson.fromJson(jsonModel, getIncomingModelType());

            var resultModel = Execute(incomingModel);
            responseModel.executionCode = ExecutionResults.SUCCESS_CODE;
            var successResultModel = new SuccessTransferResponseModel<ModelOut>();
            successResultModel.responseModel = resultModel;

            responseModel.executionResult = gson.toJson(successResultModel);
        } catch (Exception ex) {
            responseModel.executionCode = ExecutionResults.ERROR_CODE;
            var errorModel = new ErrorTransferResponseModel();
            errorModel.errorMessage = ex.getMessage();
            responseModel.executionResult = new Gson().toJson(errorModel);
        }

        return responseModel;
    }

    public abstract int GetHandlerCode();

    public abstract Type getIncomingModelType();

    protected abstract ModelOut Execute(ModelIn model) throws Exception;
}