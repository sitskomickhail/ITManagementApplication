package models.requestModels.departments;

public class CreateDepartmentsRequestModel {
    private String Title;
    private String WorkerDuties;

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
