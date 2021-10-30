import db.context.DepartmentContext;
import java.io.IOException;
import java.sql.SQLException;

public class Program {
    public static void main(String[] args) {
        try {
            var departmentsList = DepartmentContext.getDepartments();

            for (var dep : departmentsList) {
                System.out.println(dep.getId() + " " + dep.getTitle() + " " + dep.getWorkerDuties());
            }

        } catch (IOException e) {
            e.printStackTrace();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
    }
}