package managers.interfaces;

import db.context.WorkerContext;
import models.entities.Worker;

public interface IWorkerManager {
    Worker getAndValidateUser(String login, String password) throws Exception;

    void setContext(WorkerContext context);
}
