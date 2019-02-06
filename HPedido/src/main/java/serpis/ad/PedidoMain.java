package serpis.ad;
import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.List;
import java.util.Random;
import java.util.Scanner;
import java.util.function.Consumer;
import java.util.function.Function;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

public class PedidoMain {
	Scanner sc = new Scanner(System.in);

	public static void main(String[] args) {
		App.getInstance().setEntityManagerFactory(Persistence.createEntityManagerFactory("serpis.ad.hmysql"));
		
//		EntityManager entityManager = App.getInstance().getEntityManagerFactory().createEntityManager();
//		entityManager.getTransaction().begin();
//	 
//		List<Categoria> categorias =
//				entityManager.createQuery("from Categoria", Categoria.class).getResultList();
//		
//		Articulo articulo = entityManager.createQuery("from Articulo where id = 12", Articulo.class).getSingleResult();
//		articulo.setCategoria(categorias.get( new Random().nextInt(categorias.size()) ));
//		
//		//entityManager.persist(pruebaArticulo);
//		
//		show(articulo);
//		
//		update(articulo);
//		
//		entityManager.getTransaction().commit();
//		entityManager.close();
		
		List<Categoria> categorias = doInJPA(App.getInstance().getEntityManagerFactory(), entityManager -> {
			return entityManager.createQuery("select c from Categoria c order by id", Categoria.class).getResultList();
		});
		System.out.println("Artículo añadido. Pulse Enter para continuar...");
		new Scanner(System.in).nextLine();
		
		//remove(pruebaArticulo);
//		doInJPA(entityManagerFactory, entityManager2 -> {
//			Articulo articulo2 = entityManager2.getReference(Articulo.class, articulo.getId());
//			entityManager2.remove(articulo2);
//		});
		
//		Articulo articulo3 = doInJPA(App.getInstance().getEntityManagerFactory(), entityManager -> {
//			return entityManager.find(Articulo.class, 3L);
//		});
		
		Articulo articulo4 = JpaHelper.execute(entityManager -> {
			return entityManager.find(Articulo.class, 3L);
		});
		
		show(articulo4);
		
		App.getInstance().getEntityManagerFactory().close();

	}
	
	private static Articulo newArticulo() {
		Articulo pruebaArticulo = new Articulo();
		pruebaArticulo.setNombre("prueba " + LocalDateTime.now());
		pruebaArticulo.setCategoria(null);
		pruebaArticulo.setPrecio(BigDecimal.valueOf(1));
		return pruebaArticulo;
	}
	
	private static void remove (Articulo articulo) {
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		
		//articulo = entityManager.find(Articulo.class, articulo.getId());
		articulo = entityManager.getReference(Articulo.class, articulo.getId());
		
		entityManager.remove(articulo);
		
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	
	/*private static void doInJPA (EntityManagerFactory entityManagerFactory, Consumer<EntityManager> consumer) {
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		consumer.accept(entityManager);
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	
	private static <R> R doInJPA (EntityManagerFactory entityManagerFactory, Function<EntityManager, R> function) {
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		
		R result = function.apply(entityManager);
		
		entityManager.getTransaction().commit();
		entityManager.close();
		
		return result;
	}*/

	private static void update (Articulo articulo) {
		articulo.setNombre("prueba 2 " + LocalDateTime.now());
	}
	
	private static void show (Articulo articulo) {
		System.out.printf("%4s %-30s %-30s %s %n",
				articulo.getId(), articulo.getNombre(), articulo.getCategoria(), articulo.getPrecio());
	}
	
}
