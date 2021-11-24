package models.requestModels.departments;

public class UpdateDepartmentRequestModel {
    private int DepartmentId;
    private String Title;
    private String WorkerDuties;

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
}