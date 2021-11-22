package models.responseModels.projects;

import java.sql.Date;

public class ProjectGridResponseModel {
    private int ProjectId;

    private String Title;

    private String TechnologiesStack;

    private Date StartDate;

    private boolean Active;

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
        return Active;
    }

    public void setActive(boolean active) {
        Active = active;
    }
}
