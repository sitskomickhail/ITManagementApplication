package handlers.implementation.projects;

import constants.HandlerCodes;
import db.context.ProjectsContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.projects.GetDeveloperProjectsRequestModel;
import models.responseModels.projects.ProjectGridResponseModel;
import models.responseModels.projects.SearchProjectResponseModel;

import java.lang.reflect.Type;

public class GetDeveloperProjectsRequestHandler extends BaseRequestHandler<GetDeveloperProjectsRequestModel, SearchProjectResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.GET_DEVELOPER_PROJECTS;
    }

    @Override
    public Type getIncomingModelType() {
        return GetDeveloperProjectsRequestModel.class;
    }

    @Override
    protected SearchProjectResponseModel Execute(GetDeveloperProjectsRequestModel model) throws Exception {
        var projects = ProjectsContext
                .SearchProjectsByParameterAndDeveloper(model.getSearchParameter(), model.getDeveloperId());

        var responseModel = new SearchProjectResponseModel();
        for (var project : projects) {
            var projectGridModel = new ProjectGridResponseModel();

            projectGridModel.setActive(project.getActive());
            projectGridModel.setProjectId(project.getId());
            projectGridModel.setStartDate(project.getStartDate());
            projectGridModel.setTechnologiesStack(project.getTechnologiesStack());
            projectGridModel.setTitle(project.getTitle());

            responseModel.Projects.add(projectGridModel);
        }

        return responseModel;
    }
}