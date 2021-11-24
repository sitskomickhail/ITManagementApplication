package handlers.implementation.requests;

import constants.HandlerCodes;
import db.context.RequestsContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.requests.GetUserRequestsHistoryRequestModel;
import models.responseModels.requests.GetUserRequestsHistoryResponseModel;
import models.responseModels.requests.GridRequestHistoryModel;

import java.lang.reflect.Type;

public class GetUserRequestsHistoryRequestHandler extends BaseRequestHandler<GetUserRequestsHistoryRequestModel, GetUserRequestsHistoryResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.GET_USER_REQUESTS_HISTORY;
    }

    @Override
    public Type getIncomingModelType() {
        return GetUserRequestsHistoryRequestModel.class;
    }

    @Override
    protected GetUserRequestsHistoryResponseModel Execute(GetUserRequestsHistoryRequestModel model) throws Exception {
        var requests = RequestsContext.FindAllWorkerRequests(model.getWorkerId(), null, null);

        var response = new GetUserRequestsHistoryResponseModel();
        for (var request : requests) {
            var gridElement = new GridRequestHistoryModel();

            gridElement.setCreateTime(request.getCreateTime());
            gridElement.setRequestDescription(request.getRequestDescription());
            gridElement.setRequestId(request.getId());
            gridElement.setRequestStatus(request.getResolveStatus());
            gridElement.setRequestType(request.getType());

            response.RequestsList.add(gridElement);
        }

        return response;
    }
}
