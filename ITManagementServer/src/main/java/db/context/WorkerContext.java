package db.context;

import db.context.base.MySqlContext;
import helpers.PasswordHelper;
import models.entities.Worker;

import java.io.IOException;
import java.sql.*;

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
        preparedStatement.setDate(5, worker.getHireDate());
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

    public static Worker GetWorkerByLogin(String login) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM Workers WHERE Login = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setString(1, login);

        ResultSet resultSet = preparedStatement.executeQuery();

        Worker worker = null;
        while (resultSet.next()) {
            worker = new Worker();

            worker.setId(resultSet.getInt("Id"));
            worker.setName(resultSet.getString("Name"));
            worker.setPosition(resultSet.getInt("Position"));
            worker.setSalary(resultSet.getDouble("Salary"));
            worker.setBirthDate(resultSet.getDate("BirthDate"));
            worker.setHireDate(resultSet.getDate("HireDate"));
            worker.setActive(resultSet.getBoolean("Active"));
            worker.setEnglishLevel(resultSet.getString("EnglishLevel"));
            worker.setDepartmentId(resultSet.getInt("DepartmentId"));
            worker.setLogin(resultSet.getString("Login"));
            worker.setPassword(resultSet.getString("Password"));
            worker.setSalt(resultSet.getString("Salt"));
        }

        connection.close();
        return worker;
    }
}