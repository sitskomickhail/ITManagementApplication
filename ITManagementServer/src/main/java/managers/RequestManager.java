package managers;

import constants.RequestsResolveStatuses;
import constants.RequestsTypes;
import db.context.RequestsContext;
import db.context.WorkerContext;
import managers.interfaces.IRequestManger;

import java.io.IOException;
import java.sql.SQLException;
import java.util.concurrent.TimeUnit;

public class RequestManager implements IRequestManger {

    @Override
    public double CalculateWorkerVacationDays(int workerId) throws IOException, SQLException {

        var workerRequests = RequestsContext.FindAllWorkerRequests(workerId, RequestsTypes.Vacation, RequestsResolveStatuses.Solved);

        double vacatedDays = 0;

        for (var request : workerRequests) {
            var results = request.getRequestDescription().split(" ");
            vacatedDays += Double.parseDouble(results[results.length - 2]);
        }

        var worker = WorkerContext.GetWorkerById(workerId);

        var daysWorked = TimeUnit.DAYS.convert(new java.util.Date().getTime() - worker.getHireDate().getTime(), TimeUnit.MILLISECONDS);
        double calculatedVacationsForAllTime = daysWorked * 0.077;

        return calculatedVacationsForAllTime - vacatedDays;
    }
}