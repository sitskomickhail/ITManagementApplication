package models.responseModels.projects;

import java.sql.Date;
import java.util.ArrayList;

public class GetProjectByIdResponseModel {
    private String Title;
    private String Description;
    private String TechnologiesStack;
    private Date StartDate;
    private boolean IsActive;
    private ArrayList<ProjectWorkerResponseModel> Workers;

    public GetProjectByIdResponseModel() {
        Workers = new ArrayList<>();
    }

    public String getTitle() {
        return Title;
    }

    public void setTitle(String title) {
        Title = title;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public String getTechnologiesStack() {
        return TechnologiesStack;
    }

    public void setTechnologiesStack(String technologiesStack) {
        TechnologiesStack = technologiesStack;
    }

    public Date getStartDate() {
        return StartDate;
    }

    public void setStartDate(Date startDate) {
        StartDate = startDate;
    }

    public boolean isActive() {
        return IsActive;
    }

    public void setActive(boolean active) {
        IsActive = active;
    }

    public ArrayList<ProjectWorkerResponseModel> getWorkers() {
        return Workers;
    }

    public void setWorkers(ArrayList<ProjectWorkerResponseModel> workers) {
        Workers = workers;
    }
}