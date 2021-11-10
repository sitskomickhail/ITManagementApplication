package handlers.implementation.workers;

import constants.HandlerCodes;
import constants.UserRoles;
import db.context.WorkerContext;
import handlers.base.BaseRequestHandler;
import models.entities.Worker;
import models.requestModels.workers.RegisterRequestModel;
import models.responseModels.workers.RegisterResponseModel;

import java.lang.reflect.Type;

public class RegisterWorkerRequestHandler extends BaseRequestHandler<RegisterRequestModel, RegisterResponseModel> {
    private String administrator_secure_password = "cbybq hs,fr";

    @Override
    public int GetHandlerCode() {
        return HandlerCodes.REGISTER_WORKER;
    }

    @Override
    public Type getIncomingModelType() {
        return RegisterRequestModel.class;
    }

    @Override
    protected RegisterResponseModel Execute(RegisterRequestModel registerRequestModel) throws Exception {
        if (!administrator_secure_password.equals(registerRequestModel.getAdministerPassword())) {
            throw new Exception("Некорректный ключ доступа");
        }

        var foundWorker = WorkerContext.GetWorkerByLogin(registerRequestModel.getLogin());

        if (foundWorker != null) {
            throw new Exception("Пользователь с таким логином уже существует.");
        }

        Worker worker = new Worker();
        worker.setLogin(registerRequestModel.getLogin());
        worker.setPassword(registerRequestModel.getPassword());
        worker.setPosition(UserRoles.Administrator);

        WorkerContext.CreateNewWorker(worker);

        return new RegisterResponseModel();
    }
}