package managers.interfaces;

import java.io.IOException;
import java.sql.SQLException;

public interface IRequestManger {
    double CalculateWorkerVacationDays(int workerId) throws IOException, SQLException;
}