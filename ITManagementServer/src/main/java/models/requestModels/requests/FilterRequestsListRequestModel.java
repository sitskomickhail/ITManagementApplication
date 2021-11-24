package models.requestModels.requests;

import java.util.ArrayList;

public class FilterRequestsListRequestModel {
    private ArrayList<Integer> FilteringTypes;

    public ArrayList<Integer> getFilteringTypes() {
        return FilteringTypes;
    }

    public void setFilteringTypes(ArrayList<Integer> filteringTypes) {
        FilteringTypes = filteringTypes;
    }
}