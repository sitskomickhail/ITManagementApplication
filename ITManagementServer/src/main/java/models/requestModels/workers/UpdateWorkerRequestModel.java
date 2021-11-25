package models.requestModels.workers;

import java.sql.Date;

public class UpdateWorkerRequestModel {
    private int WorkerId;

    private int Position;

    private String Name;

    private double Salary;

    private Date BirthDate;

    private boolean Active;

    private String EnglishLevel;

    private String Login;

    private Integer DepartmentId;

    public int getWorkerId() {
        return WorkerId;
    }

    public void setWorkerId(int workerId) {
        WorkerId = workerId;
    }

    public int getPosition() {
        return Position;
    }

    public void setPosition(int position) {
        Position = position;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public double getSalary() {
        return Salary;
    }

    public void setSalary(double salary) {
        Salary = salary;
    }

    public Date getBirthDate() {
        return BirthDate;
    }

    public void setBirthDate(Date birthDate) {
        BirthDate = birthDate;
    }

    public boolean isActive() {
        return Active;
    }

    public void setActive(boolean active) {
        Active = active;
    }

    public String getEnglishLevel() {
        return EnglishLevel;
    }

    public void setEnglishLevel(String englishLevel) {
        EnglishLevel = englishLevel;
    }

    public String getLogin() {
        return Login;
    }

    public void setLogin(String login) {
        Login = login;
    }

    public Integer getDepartmentId() {
        return DepartmentId;
    }

    public void setDepartmentId(Integer departmentId) {
        DepartmentId = departmentId;
    }
}