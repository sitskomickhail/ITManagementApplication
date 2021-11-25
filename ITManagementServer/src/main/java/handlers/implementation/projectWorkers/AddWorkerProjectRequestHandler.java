package handlers.implementation.projectWorkers;

import constants.HandlerCodes;
import db.context.ProjectWorkersContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.projectWorkers.AddWorkerToProjectRequestModel;
import models.responseModels.projectWorkers.AddWorkerToProjectResponseModel;

import java.lang.reflect.Type;

public class AddWorkerProjectRequestHandler extends BaseRequestHandler<AddWorkerToProjectRequestModel, AddWorkerToProjectResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.ADD_WORKER_TO_PROJECT;
    }

    @Override
    public Type getIncomingModelType() {
        return AddWorkerToProjectRequestModel.class;
    }

    @Override
    protected AddWorkerToProjectResponseModel Execute(AddWorkerToProjectRequestModel model) throws Exception {
        ProjectWorkersContext.CreateProjectWorker(model.getProjectId(), model.getWorkerId());

        return new AddWorkerToProjectResponseModel();
    }
}