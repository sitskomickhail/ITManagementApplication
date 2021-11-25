package models.requestModels.projects;

import java.sql.Date;

public class CreateProjectRequestModel {
    private String Title;
    private String Description;
    private String TechnologiesStack;
    private Date StartDate;


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
}