package db.context;

import constants.RequestsResolveStatuses;
import db.context.base.MySqlContext;
import models.common.GridDepartmentContextModel;
import models.entities.Department;
import models.entities.Request;

import java.io.IOException;
import java.sql.Date;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class DepartmentContext {
    public static List<GridDepartmentContextModel> searchDepartments(String searchParameters) throws IOException, SQLException {

        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT d.Id, d.Title, d.WorkerDuties, COUNT(w.Id) as 'WorkersCount' " +
                "FROM itmanagementdb.departments as d " +
                "LEFT JOIN itmanagementdb.workers as w ON w.DepartmentId = d.Id " +
                "WHERE d.Title LIKE '%" + searchParameters + "%' OR " +
                "d.WorkerDuties LIKE '%" + searchParameters + "%' " +
                "GROUP BY d.Id";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        ResultSet resultSet = preparedStatement.executeQuery();

        List<GridDepartmentContextModel> departments = new ArrayList<>();

        while (resultSet.next()) {
            var department = fillDepartmentByResult(resultSet);

            var gridDepartment = new GridDepartmentContextModel();
            gridDepartment.setDepartmentId(department.getId());
            gridDepartment.setTitle(department.getTitle());
            gridDepartment.setWorkerDuties(department.getWorkerDuties());
            gridDepartment.setWorkersCount(resultSet.getInt("WorkersCount"));

            departments.add(gridDepartment);
        }

        return departments;
    }

    public static void createDepartment(String title, String workerDuties) throws SQLException, IOException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "INSERT INTO Departments " +
                "(`Title`, `WorkerDuties`) " +
                "VALUES (?, ?);";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);

        preparedStatement.setString(1, title);
        preparedStatement.setString(2, workerDuties);

        preparedStatement.executeUpdate();
        connection.close();
    }

    public static void deleteDepartment(int departmentId) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "Delete FROM Departments WHERE `Id` = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setInt(1, departmentId);

        preparedStatement.executeUpdate();
        connection.close();
    }

    public static void updateDepartment(Department department) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "UPDATE Departments SET Title = ?, WorkerDuties = ? WHERE `Id` = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setString(1, department.getTitle());
        preparedStatement.setString(2, department.getWorkerDuties());
        preparedStatement.setInt(3, department.getId());

        preparedStatement.executeUpdate();
        connection.close();
    }

    public static Department getDepartmentById(int id) throws SQLException, IOException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM Departments WHERE Id = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setInt(1, id);

        ResultSet resultSet = preparedStatement.executeQuery();

        Department department = null;

        while (resultSet.next()) {
            department = fillDepartmentByResult(resultSet);
        }

        return department;
    }

    private static Department fillDepartmentByResult(ResultSet resultSet) throws SQLException {
        Department department = new Department();
        department.setTitle(resultSet.getString("Title"));
        department.setWorkerDuties(resultSet.getString("WorkerDuties"));
        department.setId(resultSet.getInt("Id"));

        return department;
    }
}