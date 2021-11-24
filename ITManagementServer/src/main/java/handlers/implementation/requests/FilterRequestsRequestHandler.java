package handlers.implementation.requests;

import constants.HandlerCodes;
import db.context.RequestsContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.requests.FilterRequestsListRequestModel;
import models.responseModels.requests.FilterRequestsListResponseModel;
import models.responseModels.requests.GridRequestHistoryModel;

import java.lang.reflect.Type;

public class FilterRequestsRequestHandler extends BaseRequestHandler<FilterRequestsListRequestModel, FilterRequestsListResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.FILTER_REQUESTS_LIST;
    }

    @Override
    public Type getIncomingModelType() {
        return FilterRequestsListRequestModel.class;
    }

    @Override
    protected FilterRequestsListResponseModel Execute(FilterRequestsListRequestModel model) throws Exception {
        var requests = RequestsContext.FilterRequestsByTypes(model.getFilteringTypes());

        var response = new FilterRequestsListResponseModel();
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
