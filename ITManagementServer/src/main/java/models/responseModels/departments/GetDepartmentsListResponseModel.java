package models.responseModels.departments;

import java.util.ArrayList;

public class GetDepartmentsListResponseModel {
    public ArrayList<DepartmentGridResponseModel> Departments;

    public GetDepartmentsListResponseModel() {
        Departments = new ArrayList<>();
    }
}