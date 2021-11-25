package handlers.implementation.projects;

import constants.HandlerCodes;
import db.context.ProjectsContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.projects.CreateProjectRequestModel;
import models.responseModels.projects.CreateProjectResponseModel;

import java.lang.reflect.Type;

public class CreateProjectRequestHandler extends BaseRequestHandler<CreateProjectRequestModel, CreateProjectResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.CREATE_PROJECT;
    }

    @Override
    public Type getIncomingModelType() {
        return CreateProjectRequestModel.class;
    }

    @Override
    protected CreateProjectResponseModel Execute(CreateProjectRequestModel model) throws Exception {
        ProjectsContext.CreateProject(model.getTitle(), model.getDescription(), model.getTechnologiesStack(), model.getStartDate());

        return new CreateProjectResponseModel();
    }
}