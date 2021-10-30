package db.context.base;

import java.io.IOException;
import java.io.InputStream;
import java.net.URISyntaxException;
import java.net.URL;
import java.nio.file.Files;
import java.nio.file.Path;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Properties;

public class MySqlContext {

    private Connection connection;
    private static MySqlContext sqlContext;

    public MySqlContext() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver").newInstance();
        } catch (Exception ex) {
            System.out.println("Exception on driver searching: " + ex.getMessage());
            ex.printStackTrace();
        }
    }

    public void closeConnection() {
        if (connection != null) {
            try {
                connection.close();
            } catch (SQLException e) {
            }
        }
    }

    public Connection getConnection() throws SQLException, IOException {

        if (connection == null || connection.isClosed()) {
            Properties props = new Properties();

            URL resource = MySqlContext.class.getClassLoader().getResource("database.properties");
            try (InputStream in = Files.newInputStream(Path.of(resource.toURI()))) {
                props.load(in);
            } catch (URISyntaxException e) {
                e.printStackTrace();
            }
            String url = props.getProperty("url");
            String username = props.getProperty("username");
            String password = props.getProperty("password");

            connection = DriverManager.getConnection(url, username, password);
        }

        return connection;
    }

    public static MySqlContext getInstance() {
        if (sqlContext == null) {
            sqlContext = new MySqlContext();
        }

        return sqlContext;
    }
}