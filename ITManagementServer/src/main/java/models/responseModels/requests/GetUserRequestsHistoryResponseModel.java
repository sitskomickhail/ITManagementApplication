package models.responseModels.requests;

import java.util.ArrayList;

public class GetUserRequestsHistoryResponseModel {
    public ArrayList<GridRequestHistoryModel> RequestsList;

    public GetUserRequestsHistoryResponseModel() {
        RequestsList = new ArrayList<>();
    }
}
