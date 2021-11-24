package handlers.implementation.requests;

import constants.HandlerCodes;
import db.context.RequestsContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.requests.UpdateRequestRequestModel;
import models.responseModels.requests.UpdateRequestResponseModel;

import java.lang.reflect.Type;

public class UpdateRequestRequestHandler extends BaseRequestHandler<UpdateRequestRequestModel, UpdateRequestResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.UPDATE_REQUEST;
    }

    @Override
    public Type getIncomingModelType() {
        return UpdateRequestRequestModel.class;
    }

    @Override
    protected UpdateRequestResponseModel Execute(UpdateRequestRequestModel model) throws Exception {
        var request = RequestsContext.GetRequestById(model.getRequestId());

        request.setResolveStatus(model.getRequestStatus());
        request.setResolveNote(model.getResolveNote());

        RequestsContext.UpdateRequest(request);

        return new UpdateRequestResponseModel();
    }
}