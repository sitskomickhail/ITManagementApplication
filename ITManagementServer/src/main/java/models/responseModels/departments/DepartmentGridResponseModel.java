package models.responseModels.departments;

public class DepartmentGridResponseModel {
    private int DepartmentId;
    private String Title;
    private String WorkerDuties;
    private int WorkersCount;

    public int getDepartmentId() {
        return DepartmentId;
    }

    public void setDepartmentId(int departmentId) {
        DepartmentId = departmentId;
    }

    public String getTitle() {
        return Title;
    }

    public void setTitle(String title) {
        Title = title;
    }

    public String getWorkerDuties() {
        return WorkerDuties;
    }

    public void setWorkerDuties(String workerDuties) {
        WorkerDuties = workerDuties;
    }

    public int getWorkersCount() {
        return WorkersCount;
    }

    public void setWorkersCount(int workersCount) {
        WorkersCount = workersCount;
    }
}