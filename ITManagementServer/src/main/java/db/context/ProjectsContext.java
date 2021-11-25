package db.context;

import db.context.base.MySqlContext;
import models.common.GridProjectWorkerContextModel;
import models.common.GridWorkerContextModel;
import models.entities.Department;
import models.entities.Project;

import javax.xml.transform.Result;
import java.io.IOException;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

public class ProjectsContext {
    public static ArrayList<Project> SearchProjectsByParameter(String searchParameter) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM projects as p " +
                "WHERE p.Title LIKE '%" + searchParameter + "%' OR p.Description LIKE '%" + searchParameter + "%' OR " +
                "p.TechnologiesStack LIKE '%" + searchParameter + "%' OR p.StartDate LIKE '%" + searchParameter + "%';";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);

        ResultSet resultSet = preparedStatement.executeQuery();

        ArrayList<Project> projects = new ArrayList<>();
        while (resultSet.next()) {
            Project project = fillProjectInfoByResult(resultSet);
            projects.add(project);
        }

        connection.close();
        return projects;
    }

    public static ArrayList<Project> SearchProjectsByParameterAndDeveloper(String searchParameter, int developerId) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM projects as p " +
                "INNER JOIN itmanagementdb.workerprojects as wp ON p.Id = wp.ProjectId " +
                "INNER JOIN itmanagementdb.workers as w on wp.WorkerId = w.Id " +
                "WHERE (p.Title LIKE '%" + searchParameter + "%' OR p.Description LIKE '%" + searchParameter + "%' OR " +
                "p.TechnologiesStack LIKE '%" + searchParameter + "%' OR p.StartDate LIKE '%" + searchParameter + "%') " +
                "AND w.Id = " + developerId;

        PreparedStatement preparedStatement = connection.prepareStatement(sql);

        ResultSet resultSet = preparedStatement.executeQuery();

        ArrayList<Project> projects = new ArrayList<>();
        while (resultSet.next()) {
            Project project = fillProjectInfoByResult(resultSet);
            projects.add(project);
        }

        connection.close();
        return projects;
    }

    public static Project GetProjectById(int projectId) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM Projects WHERE Id = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setInt(1, projectId);

        ResultSet resultSet = preparedStatement.executeQuery();

        Project project = null;

        while (resultSet.next()) {
            project = fillProjectInfoByResult(resultSet);
        }

        return project;
    }

    public static void UpdateProject(Project project) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "UPDATE Projects SET Title = ?, Description = ?, TechnologiesStack = ?, StartDate = ?, Active = ? " +
                " WHERE `Id` = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setString(1, project.getTitle());
        preparedStatement.setString(2, project.getDescription());
        preparedStatement.setString(3, project.getTechnologiesStack());
        preparedStatement.setDate(4, project.getStartDate());
        preparedStatement.setBoolean(5, project.getActive());
        preparedStatement.setInt(6, project.getId());

        preparedStatement.executeUpdate();
        connection.close();
    }

    public static ArrayList<GridProjectWorkerContextModel> FilterProjectsById(int projectId) throws SQLException, IOException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT p.Id, p.Title as 'ProjectName', p.Description, p.StartDate, p.Active, p.TechnologiesStack, w.Id as 'WorkerId', w.Name, w.Position as 'UserRole', d.Title, wp.Cost " +
                "FROM itmanagementdb.projects as p " +
                "LEFT JOIN itmanagementdb.workerprojects as wp ON p.Id = wp.ProjectId " +
                "LEFT JOIN itmanagementdb.workers as w ON w.Id = wp.WorkerId " +
                "LEFT JOIN itmanagementdb.departments as d on w.DepartmentId = d.Id " +
                "WHERE p.Id = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setInt(1, projectId);

        ResultSet resultSet = preparedStatement.executeQuery();

        ArrayList<GridProjectWorkerContextModel> projectsInfo = new ArrayList<>();
        while (resultSet.next()) {
            GridProjectWorkerContextModel project = new GridProjectWorkerContextModel();

            project.setProjectId(resultSet.getInt("Id"));
            project.setProjectName(resultSet.getString("ProjectName"));
            project.setDescription(resultSet.getString("Description"));
            project.setTechnologiesStack(resultSet.getString("TechnologiesStack"));
            project.setWorkerName(resultSet.getString("Name"));
            project.setDepartment(resultSet.getString("Title"));
            project.setCost(resultSet.getDouble("Cost"));
            project.setStartDate(resultSet.getDate("StartDate"));
            project.setActive(resultSet.getBoolean("Active"));
            project.setWorkerId(resultSet.getInt("WorkerId"));
            project.setUserRole(resultSet.getInt("UserRole"));

            projectsInfo.add(project);
        }

        connection.close();
        return projectsInfo;
    }

    private static Project fillProjectInfoByResult(ResultSet resultSet) throws SQLException {
        Project project = new Project();

        project.setId(resultSet.getInt("Id"));
        project.setTitle(resultSet.getString("Title"));
        project.setDescription(resultSet.getString("Description"));
        project.setTechnologiesStack(resultSet.getString("TechnologiesStack"));
        project.setStartDate(resultSet.getDate("StartDate"));
        project.setActive(resultSet.getBoolean("Active"));

        return project;
    }
}