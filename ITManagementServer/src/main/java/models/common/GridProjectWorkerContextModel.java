package models.common;

public class GridProjectWorkerContextModel {
    private int projectId;
    private String projectName;
    private String description;
    private String technologiesStack;
    private String workerName;
    private String department;
    private Double cost;

    public int getProjectId() {
        return projectId;
    }

    public void setProjectId(int projectId) {
        this.projectId = projectId;
    }

    public String getProjectName() {
        return projectName;
    }

    public void setProjectName(String projectName) {
        this.projectName = projectName;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getTechnologiesStack() {
        return technologiesStack;
    }

    public void setTechnologiesStack(String technologiesStack) {
        this.technologiesStack = technologiesStack;
    }

    public String getWorkerName() {
        return workerName;
    }

    public void setWorkerName(String workerName) {
        this.workerName = workerName;
    }

    public String getDepartment() {
        return department;
    }

    public void setDepartment(String department) {
        this.department = department;
    }

    public Double getCost() {
        return cost;
    }

    public void setCost(Double cost) {
        this.cost = cost;
    }
}