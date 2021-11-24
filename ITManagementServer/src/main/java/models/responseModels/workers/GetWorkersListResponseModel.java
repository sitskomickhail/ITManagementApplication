package models.responseModels.workers;

import java.util.ArrayList;

public class GetWorkersListResponseModel {
    private ArrayList<WorkerGridResponseModel> WorkersList;

    public GetWorkersListResponseModel() {
        WorkersList = new ArrayList<>();
    }

    public ArrayList<WorkerGridResponseModel> getWorkersList() {
        return WorkersList;
    }

    public void setWorkersList(ArrayList<WorkerGridResponseModel> workersList) {
        WorkersList = workersList;
    }

    public void appendList(WorkerGridResponseModel gridWorker) {
        WorkersList.add(gridWorker);
    }
}