package handlers.implementation.workers;

import constants.HandlerCodes;
import db.context.WorkerContext;
import handlers.base.BaseRequestHandler;
import models.entities.Worker;
import models.requestModels.workers.CreateWorkerRequestModel;
import models.responseModels.workers.CreateWorkerResponseModel;

import java.lang.reflect.Type;
import java.sql.Date;

public class CreateWorkerRequestHandler extends BaseRequestHandler<CreateWorkerRequestModel, CreateWorkerResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.CREATE_WORKER;
    }

    @Override
    public Type getIncomingModelType() {
        return CreateWorkerRequestModel.class;
    }

    @Override
    protected CreateWorkerResponseModel Execute(CreateWorkerRequestModel model) throws Exception {
        var foundWorker = WorkerContext.GetWorkerByLogin(model.getLogin());

        if (foundWorker != null) {
            throw new Exception("Пользователь с таким логином уже существует.");
        }

        Worker worker = new Worker();
        worker.setLogin(model.getLogin());
        worker.setPassword(model.getPassword());
        worker.setName(model.getName());
        worker.setBirthDate(model.getBirthDate());
        worker.setSalary(model.getSalary());
        worker.setPosition(model.getPosition());

        WorkerContext.CreateNewWorker(worker);

        return new CreateWorkerResponseModel();
    }
}