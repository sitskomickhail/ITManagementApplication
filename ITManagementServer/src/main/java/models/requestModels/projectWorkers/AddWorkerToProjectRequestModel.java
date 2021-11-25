package models.requestModels.projectWorkers;

public class AddWorkerToProjectRequestModel {
    private int WorkerId;
    private int ProjectId;

    public int getWorkerId() {
        return WorkerId;
    }

    public void setWorkerId(int workerId) {
        WorkerId = workerId;
    }

    public int getProjectId() {
        return ProjectId;
    }

    public void setProjectId(int projectId) {
        ProjectId = projectId;
    }
}