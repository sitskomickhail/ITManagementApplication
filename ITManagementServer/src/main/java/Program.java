import models.entities.Department;
import models.entities.Worker;
import org.hibernate.Session;
import utils.HibernateUtil;

public class Program {
    public static void main(String[] args) {
        System.out.println("Maven + Hibernate + MySQL");
        Session session = HibernateUtil.getSessionFactory().openSession();

        session.beginTransaction();

        Worker netWorker = new Worker();
//        netWorker.setDepartmentId(1);
        netWorker.setPosition(1);
        netWorker.setName("Иван Шатухо");
        netWorker.setLogin("shatukho");
        netWorker.setPassword("pass");
        netWorker.setSalt("salt");

        session.save(netWorker);
        session.getTransaction().commit();
    }
}