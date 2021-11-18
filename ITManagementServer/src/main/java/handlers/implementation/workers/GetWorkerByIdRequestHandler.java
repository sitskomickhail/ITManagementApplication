package handlers.implementation.workers;

import constants.HandlerCodes;
import db.context.DepartmentContext;
import db.context.WorkerContext;
import handlers.base.BaseRequestHandler;
import models.entities.Department;
import models.requestModels.workers.GetWorkerByIdRequestModel;
import models.responseModels.workers.GetWorkerByIdResponseModel;

import java.lang.reflect.Type;

public class GetWorkerByIdRequestHandler extends BaseRequestHandler<GetWorkerByIdRequestModel, GetWorkerByIdResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.GET_WORKER_BY_ID;
    }

    @Override
    public Type getIncomingModelType() {
        return GetWorkerByIdRequestModel.class;
    }

    @Override
    protected GetWorkerByIdResponseModel Execute(GetWorkerByIdRequestModel model) throws Exception {
        var worker = WorkerContext.GetWorkerById(model.getWorkerId());

        if (worker == null) {
            throw new Exception("Worker with such id was not found");
        }

        Department department = null;
        if (worker.getDepartmentId() != null) {
            department = DepartmentContext.getDepartmentById(worker.getDepartmentId());
        }

        GetWorkerByIdResponseModel response = new GetWorkerByIdResponseModel();
        response.setWorkerId(worker.getId());
        response.setName(worker.getName());
        response.setPosition(worker.getPosition());
        response.setSalary(worker.getSalary());
        response.setBirthDate(worker.getBirthDate());
        response.setHireDate(worker.getHireDate());
        response.setActive(worker.getActive());
        response.setEnglishLevel(worker.getEnglishLevel());
        response.setLogin(worker.getLogin());
        if(department != null) {
            response.setDepartment(department.getTitle());
            response.setDepartmentId(department.getId());
        }

        return response;
    }
}