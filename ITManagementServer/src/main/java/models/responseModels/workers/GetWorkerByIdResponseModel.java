package models.responseModels.workers;

import java.sql.Date;

public class GetWorkerByIdResponseModel {
    private int WorkerId;
    private String Name;
    private int Position;
    private double Salary;
    private Date BirthDate;
    private Date HireDate;
    private boolean Active;
    private String EnglishLevel;
    private String Login;
    private String Department;
    private Integer DepartmentId;

    public int getWorkerId() {
        return WorkerId;
    }

    public void setWorkerId(int workerId) {
        WorkerId = workerId;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public int getPosition() {
        return Position;
    }

    public void setPosition(int position) {
        Position = position;
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

    public Date getHireDate() {
        return HireDate;
    }

    public void setHireDate(Date hireDate) {
        HireDate = hireDate;
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

    public String getDepartment() {
        return Department;
    }

    public void setDepartment(String department) {
        Department = department;
    }

    public Integer getDepartmentId() {
        return DepartmentId;
    }

    public void setDepartmentId(Integer departmentId) {
        DepartmentId = departmentId;
    }
}