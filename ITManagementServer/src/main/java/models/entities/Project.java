package models.entities;

import java.sql.Date;

public class Project {
    private int id;
    private String title;
    private String description;
    private String technologiesStack;
    private Date startDate;
    private Boolean active;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getTechnologiesStack() {
        return technologiesStack;
    }

    public void setTechnologiesStack(String technologiesStack) {
        this.technologiesStack = technologiesStack;
    }

    public Date getStartDate() {
        return startDate;
    }

    public void setStartDate(Date startDate) {
        this.startDate = startDate;
    }

    public Boolean getActive() {
        return active;
    }

    public void setActive(Boolean active) {
        this.active = active;
    }
}