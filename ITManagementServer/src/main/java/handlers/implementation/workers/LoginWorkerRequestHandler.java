package handlers.implementation.workers;

import constants.HandlerCodes;
import db.context.WorkerContext;
import handlers.base.BaseRequestHandler;

import helpers.PasswordHelper;
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

    @Override
    protected LoginResponseModel Execute(LoginRequestModel loginRequestModel) throws Exception {
        var worker = WorkerContext.GetWorkerByLogin(loginRequestModel.getLogin());

        if (worker == null) {
            throw new Exception("Логин или пароль неверны");
        }

        if (!PasswordHelper.VerifyUserPassword(loginRequestModel.getPassword(), worker.getSalt(), worker.getPassword())) {
            throw new Exception("Логин или пароль неверны");
        }

        LoginResponseModel model = new LoginResponseModel();
        model.setUserId(worker.getId());
        model.setUserRole(worker.getPosition());

        return model;
    }
}