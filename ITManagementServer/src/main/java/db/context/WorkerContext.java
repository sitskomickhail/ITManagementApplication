package db.context;

import db.context.base.MySqlContext;
import helpers.PasswordHelper;
import models.common.GridWorkerContextModel;
import models.entities.Worker;

import java.io.IOException;
import java.sql.*;
import java.util.ArrayList;

public class WorkerContext {
    public static void CreateNewWorker(Worker worker) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "INSERT INTO Workers " +
                "(`Name`, `Position`, `Salary`, `BirthDate`, `HireDate`, " +
                "`Active`, `EnglishLevel`, `DepartmentId`, `Login`, `Password`, `Salt`) " +
                "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);\n";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setString(1, worker.getName());
        preparedStatement.setInt(2, worker.getPosition());
        preparedStatement.setDouble(3, worker.getSalary() == null ? 0 : worker.getSalary());
        preparedStatement.setDate(4, worker.getBirthDate());
        preparedStatement.setDate(5, new Date(new java.util.Date().getTime()));
        preparedStatement.setBoolean(6, true);
        preparedStatement.setString(7, worker.getEnglishLevel());
        if (worker.getDepartmentId() == null) {
            preparedStatement.setNull(8, Types.INTEGER);
        } else {
            preparedStatement.setInt(8, worker.getDepartmentId());
        }
        preparedStatement.setString(9, worker.getLogin());

        String passwordSalt = PasswordHelper.GenerateSalt();

        preparedStatement.setString(10, PasswordHelper.GenerateSecurePassword(worker.getPassword(), passwordSalt));
        preparedStatement.setString(11, passwordSalt);

        preparedStatement.executeUpdate();
        connection.close();
    }

    public static Worker GetWorkerById(int id) throws SQLException, IOException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM Workers WHERE Id = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setInt(1, id);

        ResultSet resultSet = preparedStatement.executeQuery();

        Worker worker = null;
        while (resultSet.next()) {
            worker = fillWorkerByResult(resultSet);
        }

        connection.close();
        return worker;
    }

    public Worker GetWorkerByLogin(String login) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM Workers WHERE Login = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setString(1, login);

        ResultSet resultSet = preparedStatement.executeQuery();

        Worker worker = null;
        while (resultSet.next()) {
            worker = fillWorkerByResult(resultSet);
        }

        connection.close();
        return worker;
    }

    public static ArrayList<GridWorkerContextModel> GetWorkerByDepartmentId(int departmentId) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM Workers WHERE DepartmentId = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setInt(1, departmentId);

        ResultSet resultSet = preparedStatement.executeQuery();

        ArrayList<GridWorkerContextModel> gridWorkersList = new ArrayList<>();
        while (resultSet.next()) {
            GridWorkerContextModel gridWorker = new GridWorkerContextModel();

            gridWorker.setId(resultSet.getInt("Id"));
            gridWorker.setName(resultSet.getString("Name"));
            gridWorker.setSalary(resultSet.getDouble("Salary"));
            gridWorker.setHireDate(resultSet.getDate("HireDate"));

            gridWorkersList.add(gridWorker);
        }

        connection.close();
        return gridWorkersList;
    }

    public static ArrayList<GridWorkerContextModel> SearchWorkerByParameter(String searchParameter) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT w.Id, w.DepartmentId, w.Name, w.Salary, w.HireDate, d.Title as Department FROM itmanagementdb.workers as w " +
                "LEFT JOIN itmanagementdb.departments AS d ON d.Id = w.DepartmentId " +
                "WHERE Name LIKE '%" + searchParameter + "%' OR d.Title LIKE '%" + searchParameter + "%' OR w.Salary LIKE '%" + searchParameter + "%' OR w.HireDate LIKE '%" + searchParameter + "%';";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);

        ResultSet resultSet = preparedStatement.executeQuery();

        ArrayList<GridWorkerContextModel> gridWorkersList = new ArrayList<>();
        while (resultSet.next()) {
            GridWorkerContextModel gridWorker = new GridWorkerContextModel();

            gridWorker.setId(resultSet.getInt("Id"));
            Integer departmentId = resultSet.getInt("DepartmentId");
            if (departmentId != null && departmentId != 0) {
                gridWorker.setDepartmentId(departmentId);
            }
            gridWorker.setName(resultSet.getString("Name"));
            gridWorker.setSalary(resultSet.getDouble("Salary"));
            gridWorker.setHireDate(resultSet.getDate("HireDate"));
            gridWorker.setDepartment(resultSet.getString("Department"));

            gridWorkersList.add(gridWorker);
        }

        connection.close();
        return gridWorkersList;
    }

    public static void UpdateWorkerEntity(Worker worker) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "UPDATE Workers SET Name = ?, Position = ?, Salary = ?, " +
                "BirthDate = ?, Active = ?, EnglishLevel = ?, DepartmentId = ?, Login = ? " +
                "WHERE `Id` = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setString(1, worker.getName());
        preparedStatement.setInt(2, worker.getPosition());
        preparedStatement.setDouble(3, worker.getSalary() == null ? 0 : worker.getSalary());
        preparedStatement.setDate(4, worker.getBirthDate());
        preparedStatement.setBoolean(5, worker.getActive());
        preparedStatement.setString(6, worker.getEnglishLevel());
        if (worker.getDepartmentId() == null) {
            preparedStatement.setNull(7, Types.INTEGER);
        } else {
            preparedStatement.setInt(7, worker.getDepartmentId());
        }
        preparedStatement.setString(8, worker.getLogin());
        preparedStatement.setInt(9, worker.getId());

        preparedStatement.executeUpdate();
        connection.close();
    }

    private static Worker fillWorkerByResult(ResultSet resultSet) throws SQLException {
        var worker = new Worker();

        worker.setId(resultSet.getInt("Id"));
        worker.setName(resultSet.getString("Name"));
        worker.setPosition(resultSet.getInt("Position"));
        worker.setSalary(resultSet.getDouble("Salary"));
        worker.setBirthDate(resultSet.getDate("BirthDate"));
        worker.setHireDate(resultSet.getDate("HireDate"));
        worker.setActive(resultSet.getBoolean("Active"));
        worker.setEnglishLevel(resultSet.getString("EnglishLevel"));
        Integer departmentId = resultSet.getInt("DepartmentId");
        if (departmentId != null && departmentId != 0) {
            worker.setDepartmentId(departmentId);
        }
        worker.setLogin(resultSet.getString("Login"));
        worker.setPassword(resultSet.getString("Password"));
        worker.setSalt(resultSet.getString("Salt"));

        return worker;
    }
}