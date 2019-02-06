package serpis.ad;

import java.util.function.Consumer;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;

public class JpaHelper {
	public static void execute (Consumer<EntityManager> consumer) {
		execute(App.getInstance().getEntityManagerFactory(), consumer);
	}
	
	public static void execute (EntityManagerFactory entityManagerFactory, Consumer<EntityManager> consumer) {
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		consumer.accept(entityManager);
		entityManager.getTransaction().commit();
		entityManager.close();
	}
}
