package handlers.implementation.workers;

import constants.HandlerCodes;
import db.context.WorkerContext;
import handlers.base.BaseRequestHandler;

import helpers.PasswordHelper;
import managers.WorkerManager;
import managers.interfaces.IWorkerManager;
import models.requestModels.workers.LoginRequestModel;
import models.responseModels.workers.LoginResponseModel;

import java.lang.reflect.Type;

public class LoginWorkerRequestHandler extends BaseRequestHandler<LoginRequestModel, LoginResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.LOGIN_WORKER;
    }

    @Override
    public Type getIncomingModelType() {
        return LoginRequestModel.class;
    }

    private IWorkerManager _workerManager;

    public LoginWorkerRequestHandler() {
        _workerManager = new WorkerManager();
    }

    @Override
    protected LoginResponseModel Execute(LoginRequestModel model) throws Exception {
        var worker = _workerManager.getAndValidateUser(model.getLogin(), model.getPassword());

        LoginResponseModel responseModel = new LoginResponseModel();
        responseModel.setUserId(worker.getId());
        responseModel.setUserRole(worker.getPosition());

        return responseModel;
    }
}