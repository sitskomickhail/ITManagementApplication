package models.requestModels.projects;

public class GetDeveloperProjectsRequestModel {
    private String SearchParameter;
    private int DeveloperId;

    public String getSearchParameter() {
        return SearchParameter;
    }

    public void setSearchParameter(String searchParameter) {
        SearchParameter = searchParameter;
    }

    public int getDeveloperId() {
        return DeveloperId;
    }

    public void setDeveloperId(int developerId) {
        DeveloperId = developerId;
    }
}