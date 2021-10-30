package db.context;

import db.context.base.MySqlContext;
import models.entities.Department;

import java.io.IOException;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class DepartmentContext {
    public static List<Department> getDepartments() throws IOException, SQLException {

        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM Departments";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        ResultSet resultSet = preparedStatement.executeQuery();

        List<Department> departments = new ArrayList<>();

        while (resultSet.next()) {
            Department department = new Department();
            department.setTitle(resultSet.getString("Title"));
            department.setWorkerDuties(resultSet.getString("WorkerDuties"));
            department.setId(resultSet.getInt("Id"));

            departments.add(department);
        }

        return departments;
    }
}