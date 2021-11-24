package models.responseModels.projects;

import java.util.ArrayList;

public class SearchProjectResponseModel {
    public ArrayList<ProjectGridResponseModel> Projects;

    public SearchProjectResponseModel() {
        Projects = new ArrayList<>();
    }
}