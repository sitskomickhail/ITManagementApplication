package models.responseModels.departments;

import models.responseModels.workers.WorkerGridResponseModel;

import java.util.ArrayList;

public class GetFullDepartmentInfoResponseModel {
    private int DepartmentId;
    private String Title;
    private String WorkerDuties;
    public ArrayList<WorkerGridResponseModel> Workers;

    public GetFullDepartmentInfoResponseModel() {
        Workers = new ArrayList<>();
    }

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

    public ArrayList<WorkerGridResponseModel> getWorkers() {
        return Workers;
    }

    public void setWorkers(ArrayList<WorkerGridResponseModel> workers) {
        Workers = workers;
    }
}