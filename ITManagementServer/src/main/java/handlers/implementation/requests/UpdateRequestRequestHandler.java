package handlers.implementation.requests;

import constants.HandlerCodes;
import constants.RequestsResolveStatuses;
import constants.RequestsTypes;
import db.context.RequestsContext;
import db.context.WorkerContext;
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

        if(request.getResolveStatus() == RequestsResolveStatuses.Solved && request.getType() == RequestsTypes.Dismission) {
            var worker = WorkerContext.GetWorkerById(request.getWorkerId());
            worker.setActive(false);

            WorkerContext.UpdateWorkerEntity(worker);
        }

        return new UpdateRequestResponseModel();
    }
}