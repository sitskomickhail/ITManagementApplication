package models.responseModels.projectWorkers;

import models.responseModels.workers.WorkerGridResponseModel;

import java.util.ArrayList;
import java.util.List;

public class GetListOfAvailableDevelopersResponseModel {
    private List<WorkerGridResponseModel> AvailableDevelopers;

    public GetListOfAvailableDevelopersResponseModel() {
        AvailableDevelopers = new ArrayList<>();
    }

    public List<WorkerGridResponseModel> getAvailableDevelopers() {
        return AvailableDevelopers;
    }

    public void setAvailableDevelopers(List<WorkerGridResponseModel> availableDevelopers) {
        AvailableDevelopers = availableDevelopers;
    }
}