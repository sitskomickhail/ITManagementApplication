import db.context.WorkerContext;
import exceptions.IncorrectLoginOrPasswordException;
import exceptions.WorkerBlockedException;
import managers.WorkerManager;
import managers.interfaces.IWorkerManager;
import models.entities.Worker;
import org.junit.Before;
import org.junit.Test;

import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

public class WorkerLoginTest {
    private IWorkerManager workerManager;
    private WorkerContext workerContext;

    @Before
    public void setup() {
        workerManager = new WorkerManager();

        workerContext = mock(WorkerContext.class);
        workerManager.setContext(workerContext);
    }

    @Test(expected = NullPointerException.class)
    public void testUserLoginWithEmptyPassword() throws Exception {
        workerManager.getAndValidateUser("login", "");
    }

    @Test(expected = NullPointerException.class)
    public void testUserLoginWithEmptyLogin() throws Exception {
        workerManager.getAndValidateUser("", "password");
    }

    @Test(expected = IncorrectLoginOrPasswordException.class)
    public void testUserLoginWithIncorrectLogin() throws Exception {
        String providedLogin = "login";
        String providedPassword = "password";

        when(workerContext.GetWorkerByLogin(providedLogin)).thenReturn(null);

        workerManager.getAndValidateUser(providedLogin, providedPassword);
    }

    @Test(expected = IncorrectLoginOrPasswordException.class)
    public void testUserLoginWithIncorrectPassword() throws Exception {
        Worker worker = new Worker();
        worker.setLogin("login");
        worker.setPassword("2Fd/jAhg4n5h4MpoJZaYoM7U2IBkun0UjeOFK1UQ8bs=");
        worker.setSalt("FtJlGdmyOdaCIEQLlPACxBTvJhqCfWSyTKsUnXNkOyjHhGrvmvfKCLXCAClRAGMg");
        worker.setActive(true);

        String providedLogin = "login";
        String providedPassword = "password";

        when(workerContext.GetWorkerByLogin(providedLogin)).thenReturn(worker);

        workerManager.getAndValidateUser(providedLogin, providedPassword);
    }

    @Test(expected = WorkerBlockedException.class)
    public void testUserLoginWithDisabledAccount() throws Exception {
        Worker worker = new Worker();
        worker.setLogin("login");
        worker.setPassword("2Fd/jAhg4n5h4MpoJZaYoM7U2IBkun0UjeOFK1UQ8bs=");
        worker.setSalt("FtJlGdmyOdaCIEQLlPACxBTvJhqCfWSyTKsUnXNkOyjHhGrvmvfKCLXCAClRAGMg");
        worker.setActive(false);

        String providedLogin = "login";
        String providedPassword = "12345678";

        when(workerContext.GetWorkerByLogin(providedLogin)).thenReturn(worker);

        workerManager.getAndValidateUser(providedLogin, providedPassword);
    }
}