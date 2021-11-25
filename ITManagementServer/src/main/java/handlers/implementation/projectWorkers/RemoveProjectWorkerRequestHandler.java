package handlers.implementation.projectWorkers;

import constants.HandlerCodes;
import db.context.ProjectWorkersContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.projectWorkers.RemoveWorkerFromProjectRequestModel;
import models.responseModels.projectWorkers.RemoveWorkerFromProjectResponseModel;

import java.lang.reflect.Type;

public class RemoveProjectWorkerRequestHandler extends BaseRequestHandler<RemoveWorkerFromProjectRequestModel, RemoveWorkerFromProjectResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.REMOVE_WORKER_FROM_PROJECT;
    }

    @Override
    public Type getIncomingModelType() {
        return RemoveWorkerFromProjectRequestModel.class;
    }

    @Override
    protected RemoveWorkerFromProjectResponseModel Execute(RemoveWorkerFromProjectRequestModel model) throws Exception {
        ProjectWorkersContext.DeleteWorkerProject(model.getWorkerId(), model.getProjectId());

        return new RemoveWorkerFromProjectResponseModel();
    }
}