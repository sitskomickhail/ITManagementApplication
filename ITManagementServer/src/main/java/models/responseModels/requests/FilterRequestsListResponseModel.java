package models.responseModels.requests;

import java.util.ArrayList;

public class FilterRequestsListResponseModel {
    public ArrayList<GridRequestHistoryModel> RequestsList;

    public FilterRequestsListResponseModel() {
        RequestsList = new ArrayList<>();
    }
}