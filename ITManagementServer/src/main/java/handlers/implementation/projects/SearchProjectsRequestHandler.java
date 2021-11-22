package handlers.implementation.projects;

import constants.HandlerCodes;
import db.context.ProjectsContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.projects.SearchProjectsRequestModel;
import models.responseModels.projects.ProjectGridResponseModel;
import models.responseModels.projects.SearchProjectResponseModel;

import java.lang.reflect.Type;

public class SearchProjectsRequestHandler extends BaseRequestHandler<SearchProjectsRequestModel, SearchProjectResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.GET_PROJECTS_LIST;
    }

    @Override
    public Type getIncomingModelType() {
        return SearchProjectsRequestModel.class;
    }

    @Override
    protected SearchProjectResponseModel Execute(SearchProjectsRequestModel model) throws Exception {
        var projects = ProjectsContext.SearchProjectsByParameter(model.getSearchParameter());

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