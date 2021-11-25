package handlers.implementation.projectWorkers;

import constants.HandlerCodes;
import db.context.ProjectWorkersContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.projectWorkers.GetListOfAvailableDevelopersRequestModel;
import models.responseModels.projectWorkers.GetListOfAvailableDevelopersResponseModel;
import models.responseModels.workers.WorkerGridResponseModel;

import java.lang.reflect.Type;

public class GetListOfAvailableDevelopersRequestHandler extends BaseRequestHandler<GetListOfAvailableDevelopersRequestModel, GetListOfAvailableDevelopersResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.GET_LIST_OF_AVAILABLE_DEVELOPERS;
    }

    @Override
    public Type getIncomingModelType() {
        return GetListOfAvailableDevelopersRequestModel.class;
    }

    @Override
    protected GetListOfAvailableDevelopersResponseModel Execute(GetListOfAvailableDevelopersRequestModel model) throws Exception {
        var gridWorkers = ProjectWorkersContext.GetListOfAvailableDevelopers(model.getProjectId());

        var response = new GetListOfAvailableDevelopersResponseModel();

        for (var worker : gridWorkers) {
            WorkerGridResponseModel gridWorker = new WorkerGridResponseModel();

            gridWorker.setWorkerId(worker.getId());
            gridWorker.setDepartmentId(worker.getDepartmentId());
            gridWorker.setDepartment(worker.getDepartment());
            gridWorker.setHireDate(worker.getHireDate());
            gridWorker.setName(worker.getName());
            gridWorker.setSalary(worker.getSalary());

            response.getAvailableDevelopers().add(gridWorker);
        }

        return response;
    }
}