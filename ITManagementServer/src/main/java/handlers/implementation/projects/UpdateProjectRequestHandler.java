package handlers.implementation.projects;

import constants.HandlerCodes;
import db.context.ProjectsContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.projects.UpdateProjectRequestModel;
import models.responseModels.projects.UpdateProjectResponseModel;

import java.lang.reflect.Type;

public class UpdateProjectRequestHandler extends BaseRequestHandler<UpdateProjectRequestModel, UpdateProjectResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.UPDATE_PROJECT;
    }

    @Override
    public Type getIncomingModelType() {
        return UpdateProjectRequestModel.class;
    }

    @Override
    protected UpdateProjectResponseModel Execute(UpdateProjectRequestModel model) throws Exception {
        var project = ProjectsContext.GetProjectById(model.getProjectId());

        if (project == null) {
            throw new Exception("Данный проект не найден!");
        }

        project.setActive(model.isActive());
        project.setStartDate(model.getStartDate());
        project.setTechnologiesStack(model.getTechnologiesStack());
        project.setDescription(model.getDescription());
        project.setTitle(model.getTitle());

        ProjectsContext.UpdateProject(project);

        return new UpdateProjectResponseModel();
    }
}