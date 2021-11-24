package handlers.implementation.workers;

import constants.HandlerCodes;
import db.context.WorkerContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.workers.UpdateWorkerRequestModel;
import models.responseModels.workers.UpdateWorkerResponseModel;

import java.lang.reflect.Type;

public class UpdateWorkerRequestHandler extends BaseRequestHandler<UpdateWorkerRequestModel, UpdateWorkerResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.UPDATE_WORKER;
    }

    @Override
    public Type getIncomingModelType() {
        return UpdateWorkerRequestModel.class;
    }

    @Override
    protected UpdateWorkerResponseModel Execute(UpdateWorkerRequestModel model) throws Exception {
        var worker = WorkerContext.GetWorkerById(model.getWorkerId());

        if (worker == null) {
            throw new Exception("Worker with such id was not found");
        }

        worker.setPosition(model.getPosition());
        worker.setName(model.getName());
        worker.setBirthDate(model.getBirthDate());
        worker.setSalary(model.getSalary());
        worker.setActive(model.isActive());
        worker.setEnglishLevel(model.getEnglishLevel());
        worker.setLogin(model.getLogin());

        WorkerContext.UpdateWorkerEntity(worker);

        UpdateWorkerResponseModel response = new UpdateWorkerResponseModel();
        response.setActive(worker.getActive());
        response.setBirthDate(worker.getBirthDate());
        response.setEnglishLevel(worker.getEnglishLevel());
        response.setLogin(worker.getLogin());
        response.setName(worker.getName());
        response.setSalary(worker.getSalary());
        response.setPosition(worker.getPosition());
        response.setWorkerId(worker.getId());

        return response;
    }
}