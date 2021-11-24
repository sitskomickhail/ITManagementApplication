package handlers.implementation.departments;

import constants.HandlerCodes;
import db.context.DepartmentContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.departments.GetDepartmentsListRequestModel;
import models.responseModels.departments.DepartmentGridResponseModel;
import models.responseModels.departments.GetDepartmentsListResponseModel;

import java.lang.reflect.Type;

public class GetDepartmentsListRequestHandler extends BaseRequestHandler<GetDepartmentsListRequestModel, GetDepartmentsListResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.GET_DEPARTMENTS_LIST;
    }

    @Override
    public Type getIncomingModelType() {
        return GetDepartmentsListRequestModel.class;
    }

    @Override
    protected GetDepartmentsListResponseModel Execute(GetDepartmentsListRequestModel model) throws Exception {
        var departments = DepartmentContext.searchDepartments(model.getSearchParameter());

        var response = new GetDepartmentsListResponseModel();

        for (var department : departments) {
            var respDepartment = new DepartmentGridResponseModel();

            respDepartment.setDepartmentId(department.getDepartmentId());
            respDepartment.setTitle(department.getTitle());
            respDepartment.setWorkerDuties(department.getWorkerDuties());
            respDepartment.setWorkersCount(department.getWorkersCount());

            response.Departments.add(respDepartment);
        }

        return response;
    }
}
