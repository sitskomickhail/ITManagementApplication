package models.requestModels.projects;

import java.sql.Date;

public class UpdateProjectRequestModel {
    private int ProjectId;
    private String Title;
    private String Description;
    private String TechnologiesStack;
    private Date StartDate;
    private boolean IsActive;

    public int getProjectId() {
        return ProjectId;
    }

    public void setProjectId(int projectId) {
        ProjectId = projectId;
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
}