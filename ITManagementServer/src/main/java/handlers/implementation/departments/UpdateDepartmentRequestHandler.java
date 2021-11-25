package handlers.implementation.departments;

import constants.HandlerCodes;
import db.context.DepartmentContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.departments.UpdateDepartmentRequestModel;
import models.responseModels.departments.UpdateDepartmentResponseModel;

import java.lang.reflect.Type;

public class UpdateDepartmentRequestHandler extends BaseRequestHandler<UpdateDepartmentRequestModel, UpdateDepartmentResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.UPDATE_DEPARTMENT;
    }

    @Override
    public Type getIncomingModelType() {
        return UpdateDepartmentRequestModel.class;
    }

    @Override
    protected UpdateDepartmentResponseModel Execute(UpdateDepartmentRequestModel model) throws Exception {
        var department = DepartmentContext.getDepartmentById(model.getDepartmentId());

        if(department == null) {
            throw new Exception("Отдел не был найдет");
        }

        department.setWorkerDuties(model.getWorkerDuties());
        department.setTitle(model.getTitle());

        DepartmentContext.updateDepartment(department);

        return new UpdateDepartmentResponseModel();
    }
}