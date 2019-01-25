package serpis.ad;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.List;
import java.util.Random;
import java.util.Scanner;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

public class PedidoMain {
	private static EntityManagerFactory entityManagerFactory;

	public static void main(String[] args) {
		entityManagerFactory = Persistence.createEntityManagerFactory("serpis.ad.hmysql");
		
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
	 
		List<Categoria> categorias =
				entityManager.createQuery("from Categoria", Categoria.class).getResultList();
		
		Articulo pruebaArticulo = newArticulo();
		pruebaArticulo.setCategoria(categorias.get( new Random().nextInt(categorias.size()) ));
		
		entityManager.persist(pruebaArticulo);
		
		show(pruebaArticulo);
		
		entityManager.getTransaction().commit();
		entityManager.close();
		
		System.out.println("Artículo añadido. Pulse Enter para continuar...");
		new Scanner(System.in).nextLine();
		
		remove(pruebaArticulo);
		
		entityManagerFactory.close();

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

	private static void show (Articulo articulo) {
		System.out.printf("%4s %-30s %-30s %s %n",
				articulo.getId(), articulo.getNombre(), articulo.getCategoria(), articulo.getPrecio());
	}
	
}
