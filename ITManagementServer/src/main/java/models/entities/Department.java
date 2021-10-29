package models.entities;

public class Department {
    private int id;
    private String title;
    private String workerDuties;

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

    public String getWorkerDuties() {
        return workerDuties;
    }

    public void setWorkerDuties(String workerDuties) {
        this.workerDuties = workerDuties;
    }
}