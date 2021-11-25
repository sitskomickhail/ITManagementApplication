package handlers.implementation.departments;

import constants.HandlerCodes;
import db.context.DepartmentContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.departments.CreateDepartmentsRequestModel;
import models.responseModels.departments.CreateDepartmentResponseModel;

import java.lang.reflect.Type;

public class CreateDepartmentRequestHandler extends BaseRequestHandler<CreateDepartmentsRequestModel, CreateDepartmentResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.CREATE_DEPARTMENT;
    }

    @Override
    public Type getIncomingModelType() {
        return CreateDepartmentsRequestModel.class;
    }

    @Override
    protected CreateDepartmentResponseModel Execute(CreateDepartmentsRequestModel model) throws Exception {
        DepartmentContext.createDepartment(model.getTitle(), model.getWorkerDuties());

        return new CreateDepartmentResponseModel();
    }
}
