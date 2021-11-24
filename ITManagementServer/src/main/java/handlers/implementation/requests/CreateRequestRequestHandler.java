package handlers.implementation.requests;

import constants.HandlerCodes;
import db.context.RequestsContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.requests.CreateRequestRequestModel;
import models.responseModels.requests.CreateRequestResponseModel;

import java.lang.reflect.Type;

public class CreateRequestRequestHandler extends BaseRequestHandler<CreateRequestRequestModel, CreateRequestResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.CREATE_REQUEST;
    }

    @Override
    public Type getIncomingModelType() {
        return CreateRequestRequestModel.class;
    }

    @Override
    protected CreateRequestResponseModel Execute(CreateRequestRequestModel model) throws Exception {
        RequestsContext.CreateRequest(model.getRequestType(), model.getRequestDescription(), model.getWorkerId());
        return new CreateRequestResponseModel();
    }
}
