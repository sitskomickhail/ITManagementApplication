package handlers.implementation.requests;

import constants.HandlerCodes;
import db.context.RequestsContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.requests.ChangeRequestResolvingStatusRequestModel;
import models.responseModels.requests.ChangeRequestResolvingStatusResponseModel;

import java.lang.reflect.Type;

public class ChangeRequestResolvingStatusRequestHandler extends BaseRequestHandler<ChangeRequestResolvingStatusRequestModel, ChangeRequestResolvingStatusResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.CHANGE_REQUEST_STATUS;
    }

    @Override
    public Type getIncomingModelType() {
        return ChangeRequestResolvingStatusRequestModel.class;
    }

    @Override
    protected ChangeRequestResolvingStatusResponseModel Execute(ChangeRequestResolvingStatusRequestModel model) throws Exception {
        var request = RequestsContext.GetRequestById(model.getRequestId());

        request.setResolveStatus(model.getRequestStatus());
        RequestsContext.UpdateRequest(request);

        return new ChangeRequestResolvingStatusResponseModel();
    }
}