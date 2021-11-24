package handlers.implementation.departments;

import constants.HandlerCodes;
import db.context.DepartmentContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.departments.DeleteDepartmentRequestModel;
import models.responseModels.departments.DeleteDepartmentResponseModel;

import java.lang.reflect.Type;

public class RemoveDepartmentRequestHandler extends BaseRequestHandler<DeleteDepartmentRequestModel, DeleteDepartmentResponseModel> {

    @Override
    public int GetHandlerCode() {
        return HandlerCodes.DELETE_DEPARTMENT;
    }

    @Override
    public Type getIncomingModelType() {
        return DeleteDepartmentRequestModel.class;
    }

    @Override
    protected DeleteDepartmentResponseModel Execute(DeleteDepartmentRequestModel model) throws Exception {
        DepartmentContext.deleteDepartment(model.getDepartmentId());

        return new DeleteDepartmentResponseModel();
    }
}