package db.context;

import constants.RequestsResolveStatuses;
import db.context.base.MySqlContext;
import helpers.PasswordHelper;
import models.entities.Request;

import java.io.IOException;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class RequestsContext {

    public static void CreateRequest(int requestType, String requestDescription, int workerId) throws SQLException, IOException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "INSERT INTO Requests " +
                "(`CreateTime`, `Type`, `RequestDescription`, `ResolveStatus`, `ResolveNote`, `WorkerId`) " +
                "VALUES (?, ?, ?, ?, ?, ?);";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);

        preparedStatement.setDate(1, new Date(new java.util.Date().getTime()));
        preparedStatement.setInt(2, requestType);
        preparedStatement.setString(3, requestDescription);
        preparedStatement.setInt(4, RequestsResolveStatuses.Opened);
        preparedStatement.setString(5, null);
        preparedStatement.setInt(6, workerId);

        preparedStatement.executeUpdate();
        connection.close();
    }

    public static void UpdateRequest(Request request) throws IOException, SQLException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "UPDATE Requests SET Type = ?, RequestDescription = ?, ResolveStatus = ?, " +
                "ResolveNote = ?, WorkerId = ? WHERE `Id` = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setInt(1, request.getType());
        preparedStatement.setString(2, request.getRequestDescription());
        preparedStatement.setInt(3, request.getResolveStatus());
        preparedStatement.setString(4, request.getResolveNote());
        preparedStatement.setInt(5, request.getWorkerId());
        preparedStatement.setInt(6, request.getId());

        preparedStatement.executeUpdate();
        connection.close();
    }

    public static Request GetRequestById(int id) throws SQLException, IOException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM Requests WHERE Id = ?";

        PreparedStatement preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setInt(1, id);

        ResultSet resultSet = preparedStatement.executeQuery();

        Request request = null;
        while (resultSet.next()) {
            request = fillRequestByResult(resultSet);
        }

        connection.close();
        return request;
    }

    public static List<Request> FindAllWorkerRequests(int workerId, Integer requestType, Integer requestStatus) throws SQLException, IOException {
        var connection = MySqlContext.getInstance().getConnection();

        String sql = "SELECT * FROM Requests WHERE WorkerId = ?";
        PreparedStatement preparedStatement;

        if (requestType != null) {
            sql += " AND Type = ?";
        }

        if (requestStatus != null) {
            sql += " AND ResolveStatus = ?";
        }

        preparedStatement = connection.prepareStatement(sql);
        preparedStatement.setInt(1, workerId);

        if (requestType != null) {
            preparedStatement.setInt(2, requestType);
        }

        if (requestStatus != null) {
            preparedStatement.setInt(requestType != null ? 3 : 2, requestStatus);
        }

        ResultSet resultSet = preparedStatement.executeQuery();

        ArrayList<Request> requestsList = new ArrayList<>();
        while (resultSet.next()) {
            requestsList.add(fillRequestByResult(resultSet));
        }

        connection.close();
        return requestsList;
    }

    private static Request fillRequestByResult(ResultSet resultSet) throws SQLException {
        var request = new Request();

        request.setId(resultSet.getInt("Id"));
        request.setCreateTime(resultSet.getDate("CreateTime"));
        request.setRequestDescription(resultSet.getString("RequestDescription"));
        request.setResolveNote(resultSet.getString("ResolveNote"));
        request.setType(resultSet.getInt("Type"));
        request.setResolveStatus(resultSet.getInt("ResolveStatus"));
        request.setWorkerId(resultSet.getInt("WorkerId"));

        return request;
    }
}