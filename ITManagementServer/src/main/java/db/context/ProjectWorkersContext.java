package db.context;

import constants.UserRoles;
import db.context.base.MySqlContext;
import models.common.GridWorkerContextModel;
import models.entities.Department;
import models.entities.WorkerProject;

import java.io.IOException;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

public class ProjectWorkersContext {
    public static void CreateProjectWorker(int projectId, int workerId) throws SQLException, IOException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "INSERT INTO workerprojects " +
                "(`WorkerId`, `ProjectId`, `Cost`) " +
                "VALUES (?, ?, ?);";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);

        preparedStatement.setInt(1, workerId);
        preparedStatement.setInt(2, projectId);
        preparedStatement.setDouble(3, 0);

        preparedStatement.executeUpdate();
        connection.close();
    }

    public static ArrayList<GridWorkerContextModel> GetListOfAvailableDevelopers(int projectId) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT w.Id, w.DepartmentId, w.Name, w.Salary, w.HireDate, d.Title as Department  FROM itmanagementdb.workers as w " +
                "LEFT JOIN itmanagementdb.workerprojects as wp on wp.WorkerId = w.Id AND wp.ProjectId = " + projectId +
                " LEFT JOIN itmanagementdb.departments AS d ON d.Id = w.DepartmentId " +
                "WHERE w.Position = " + UserRoles.Developer + " AND wp.ProjectId IS NULL;";

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

    public static void DeleteWorkerProject(int workerId, int projectId) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "Delete FROM workerprojects WHERE `WorkerId` = ? AND `ProjectId` = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setInt(1, workerId);
        preparedStatement.setInt(2, projectId);

        preparedStatement.executeUpdate();
        connection.close();
    }

    public static void UpdateProjectWorker(WorkerProject workerProject) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "UPDATE itmanagementdb.workerprojects SET Cost = ? WHERE WorkerId = ? AND ProjectId = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setDouble(1, workerProject.getCost());
        preparedStatement.setInt(2, workerProject.getWorkerId());
        preparedStatement.setInt(3, workerProject.getProjectId());

        preparedStatement.executeUpdate();
        connection.close();
    }

    public static WorkerProject GetProjectWorker(int workerId, int projectId) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM workerprojects WHERE WorkerId = ? AND ProjectId = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setInt(1, workerId);
        preparedStatement.setInt(2, projectId);

        ResultSet resultSet = preparedStatement.executeQuery();

        WorkerProject workerProject = null;

        while (resultSet.next()) {
            workerProject = new WorkerProject();
            workerProject.setProjectId(resultSet.getInt("ProjectId"));
            workerProject.setWorkerId(resultSet.getInt("WorkerId"));
            workerProject.setCost(resultSet.getDouble("Cost"));
        }

        return workerProject;
    }
}
