package models.common;

public class GridDepartmentContextModel {
    private int departmentId;
    private String title;
    private String workerDuties;
    private int workersCount;

    public int getDepartmentId() {
        return departmentId;
    }

    public void setDepartmentId(int departmentId) {
        this.departmentId = departmentId;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getWorkerDuties() {
        return workerDuties;
    }

    public void setWorkerDuties(String workerDuties) {
        this.workerDuties = workerDuties;
    }

    public int getWorkersCount() {
        return workersCount;
    }

    public void setWorkersCount(int workersCount) {
        this.workersCount = workersCount;
    }
}