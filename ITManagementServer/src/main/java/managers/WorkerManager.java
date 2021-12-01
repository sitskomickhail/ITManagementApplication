package managers;

import db.context.WorkerContext;
import exceptions.IncorrectLoginOrPasswordException;
import exceptions.WorkerBlockedException;
import helpers.PasswordHelper;
import managers.interfaces.IWorkerManager;
import models.entities.Worker;

public class WorkerManager implements IWorkerManager {

    private WorkerContext _workerContext;

    public WorkerManager() {
        _workerContext = new WorkerContext();
    }

    public Worker getAndValidateUser(String login, String password) throws Exception {
        if (login.isEmpty() || password.isEmpty()) {
            throw new NullPointerException("Поля login и password не могут быть пустыми");
        }

        var worker = _workerContext.GetWorkerByLogin(login);

        if (worker == null) {
            throw new IncorrectLoginOrPasswordException("Неверный логин или пароль");
        }

        if (!PasswordHelper.VerifyUserPassword(password, worker.getSalt(), worker.getPassword())) {
            throw new IncorrectLoginOrPasswordException("Неверный логин или пароль");
        }

        if(!worker.getActive()) {
            throw new WorkerBlockedException("Пользователь заблокирован");
        }

        return worker;
    }

    @Override
    public void setContext(WorkerContext context) {
        this._workerContext = context;
    }
}
