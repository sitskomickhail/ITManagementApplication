package handlers.implementation.projects;

import constants.HandlerCodes;
import constants.UserRoles;
import db.context.ProjectsContext;
import handlers.base.BaseRequestHandler;
import models.requestModels.projects.GetProjectByIdRequestModel;
import models.responseModels.projects.GetProjectByIdResponseModel;
import models.responseModels.projects.ProjectWorkerResponseModel;

import java.lang.reflect.Type;

public class GetProjectByIdRequestHandler extends BaseRequestHandler<GetProjectByIdRequestModel, GetProjectByIdResponseModel> {
    @Override
    public int GetHandlerCode() {
        return HandlerCodes.GET_PROJECT_BY_ID;
    }

    @Override
    public Type getIncomingModelType() {
        return GetProjectByIdRequestModel.class;
    }

    @Override
    protected GetProjectByIdResponseModel Execute(GetProjectByIdRequestModel model) throws Exception {
        var projectInfoList = ProjectsContext.FilterProjectsById(model.getProjectId());

        var responseModel = new GetProjectByIdResponseModel();

        for (var projectInfo : projectInfoList) {
            responseModel.setDescription(projectInfo.getDescription());
            responseModel.setTechnologiesStack(projectInfo.getTechnologiesStack());
            responseModel.setTitle(projectInfo.getProjectName());
            responseModel.setActive(projectInfo.isActive());
            responseModel.setStartDate(projectInfo.getStartDate());

            if (projectInfo.getWorkerName() != null && projectInfo.getUserRole() != UserRoles.Administrator) {
                var projectWorker = new ProjectWorkerResponseModel();

                projectWorker.setCost(projectInfo.getCost());
                projectWorker.setDepartment(projectInfo.getDepartment());
                projectWorker.setName(projectInfo.getWorkerName());
                projectWorker.setWorkerId(projectInfo.getWorkerId());

                responseModel.getWorkers().add(projectWorker);
            }
        }

        return responseModel;
    }
}