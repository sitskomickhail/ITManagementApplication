package handlers.implementation.workers;

import constants.HandlerCodes;
import db.context.WorkerContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.workers.GetWorkersListRequestModel;
import models.responseModels.workers.GetWorkersListResponseModel;
import models.responseModels.workers.WorkerGridResponseModel;

import java.lang.reflect.Type;
import java.text.SimpleDateFormat;

public class GetWorkersListRequestHandler extends BaseRequestHandler<GetWorkersListRequestModel, GetWorkersListResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.GET_WORKERS_LIST;
    }

    @Override
    public Type getIncomingModelType() {
        return GetWorkersListRequestModel.class;
    }

    @Override
    protected GetWorkersListResponseModel Execute(GetWorkersListRequestModel model) throws Exception {
        GetWorkersListResponseModel responseModel = new GetWorkersListResponseModel();

        var workersList = WorkerContext.SearchWorkerByParameter(model.getSearchParameter());

        for (var worker : workersList) {
            WorkerGridResponseModel gridWorker = new WorkerGridResponseModel();

            gridWorker.setWorkerId(worker.getId());
            gridWorker.setDepartmentId(worker.getDepartmentId());
            gridWorker.setDepartment(worker.getDepartment());
            gridWorker.setHireDate(worker.getHireDate());
            gridWorker.setName(worker.getName());
            gridWorker.setSalary(worker.getSalary());

            responseModel.appendList(gridWorker);
        }

        return responseModel;
    }
}