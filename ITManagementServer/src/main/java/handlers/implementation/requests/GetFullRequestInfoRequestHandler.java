package handlers.implementation.requests;

import constants.HandlerCodes;
import db.context.RequestsContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.requests.GetRequestFullInfoRequestModel;
import models.responseModels.requests.GetRequestFullInfoResponseModel;

import java.lang.reflect.Type;

public class GetFullRequestInfoRequestHandler extends BaseRequestHandler<GetRequestFullInfoRequestModel, GetRequestFullInfoResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.GET_FULL_REQUEST_INFO;
    }

    @Override
    public Type getIncomingModelType() {
        return GetRequestFullInfoRequestModel.class;
    }

    @Override
    protected GetRequestFullInfoResponseModel Execute(GetRequestFullInfoRequestModel model) throws Exception {
        var request = RequestsContext.GetRequestById(model.getRequestId());

        var response = new GetRequestFullInfoResponseModel();
        response.setRequestDescription(request.getRequestDescription());
        response.setRequestStatus(request.getResolveStatus());
        response.setRequestType(request.getType());
        response.setResolveNotes(request.getResolveNote());

        return response;
    }
}
