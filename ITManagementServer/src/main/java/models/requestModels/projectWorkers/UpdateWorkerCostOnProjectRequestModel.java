package models.requestModels.projectWorkers;

public class UpdateWorkerCostOnProjectRequestModel {
    private int ProjectId;
    private int WorkerId;
    private double Cost;

    public int getProjectId() {
        return ProjectId;
    }

    public void setProjectId(int projectId) {
        ProjectId = projectId;
    }

    public int getWorkerId() {
        return WorkerId;
    }

    public void setWorkerId(int workerId) {
        WorkerId = workerId;
    }

    public double getCost() {
        return Cost;
    }

    public void setCost(double cost) {
        Cost = cost;
    }
}