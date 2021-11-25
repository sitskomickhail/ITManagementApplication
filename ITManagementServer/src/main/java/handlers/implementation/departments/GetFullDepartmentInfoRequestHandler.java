package handlers.implementation.departments;

import constants.HandlerCodes;
import db.context.DepartmentContext;
import db.context.WorkerContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.departments.GetFullDepartmentInfoRequestModel;
import models.responseModels.departments.GetFullDepartmentInfoResponseModel;
import models.responseModels.workers.WorkerGridResponseModel;

import java.lang.reflect.Type;

public class GetFullDepartmentInfoRequestHandler extends BaseRequestHandler<GetFullDepartmentInfoRequestModel, GetFullDepartmentInfoResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.GET_DEPARTMENT_FULL_INFO;
    }

    @Override
    public Type getIncomingModelType() {
        return GetFullDepartmentInfoRequestModel.class;
    }

    @Override
    protected GetFullDepartmentInfoResponseModel Execute(GetFullDepartmentInfoRequestModel model) throws Exception {
        var department = DepartmentContext.getDepartmentById(model.getDepartmentId());

        var response = new GetFullDepartmentInfoResponseModel();

        response.setDepartmentId(department.getId());
        response.setTitle(department.getTitle());
        response.setWorkerDuties(department.getWorkerDuties());

        var workers = WorkerContext.GetWorkerByDepartmentId(model.getDepartmentId());
        for (var worker : workers) {
            WorkerGridResponseModel gridWorker = new WorkerGridResponseModel();

            gridWorker.setWorkerId(worker.getId());
            gridWorker.setName(worker.getName());
            gridWorker.setSalary(worker.getSalary());
            gridWorker.setHireDate(worker.getHireDate());

            response.Workers.add(gridWorker);
        }

        return response;
    }
}