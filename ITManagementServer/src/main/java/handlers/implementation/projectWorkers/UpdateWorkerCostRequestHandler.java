package handlers.implementation.projectWorkers;

import constants.HandlerCodes;
import db.context.ProjectWorkersContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.projectWorkers.UpdateWorkerCostOnProjectRequestModel;
import models.responseModels.projectWorkers.UpdateWorkerCostOnProjectResponseModel;

import java.lang.reflect.Type;

public class UpdateWorkerCostRequestHandler extends BaseRequestHandler<UpdateWorkerCostOnProjectRequestModel, UpdateWorkerCostOnProjectResponseModel> {

    @Override
    public int GetHandlerCode() {
        return HandlerCodes.UPDATE_WORKER_PROJECT_COST;
    }

    @Override
    public Type getIncomingModelType() {
        return UpdateWorkerCostOnProjectRequestModel.class;
    }

    @Override
    protected UpdateWorkerCostOnProjectResponseModel Execute(UpdateWorkerCostOnProjectRequestModel model) throws Exception {
        var workerProject = ProjectWorkersContext.GetProjectWorker(model.getWorkerId(), model.getProjectId());

        if (workerProject == null) {
            throw new Exception("Данный работник на проекте не найден!");
        }

        workerProject.setCost(model.getCost());

        ProjectWorkersContext.UpdateProjectWorker(workerProject);

        return new UpdateWorkerCostOnProjectResponseModel();
    }
}