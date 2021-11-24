package models.common;

import java.sql.Date;

public class GridWorkerContextModel {
    private int Id;
    private Integer DepartmentId;
    private String Name;
    private String Department;
    private double Salary;
    private Date HireDate;

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public Integer getDepartmentId() {
        return DepartmentId;
    }

    public void setDepartmentId(Integer departmentId) {
        DepartmentId = departmentId;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getDepartment() {
        return Department;
    }

    public void setDepartment(String department) {
        Department = department;
    }

    public double getSalary() {
        return Salary;
    }

    public void setSalary(double salary) {
        Salary = salary;
    }

    public Date getHireDate() {
        return HireDate;
    }

    public void setHireDate(Date hireDate) {
        HireDate = hireDate;
    }
}