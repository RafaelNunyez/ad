package serpis.ad;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.List;
import java.util.Scanner;

import javax.persistence.Persistence;
import javax.persistence.PersistenceException;

public class PedidoMain {

	public static void main(String[] args) {
		
		try {
			execute();
		} catch (PersistenceException ex) {
			//Informar al usuario
			PersistenceExceptionHelper.show(ex);
		}
	}
	
	private static void execute () {
		App.getInstance().setEntityManagerFactory(Persistence.createEntityManagerFactory("serpis.ad.hmysql"));
		
		List<Categoria> categorias = JpaHelper.execute(entityManager -> {
			return entityManager.createQuery("select c from Categoria c order by id", Categoria.class).getResultList();
		});
		
		for (Categoria categoria : categorias)
			System.out.printf("%4s %s %n", categoria.getId(), categoria.getNombre());
		
		System.out.println("Artículo añadido. Pulse Enter para continuar...");
		new Scanner(System.in).nextLine();
		
		JpaHelper.execute(entityManager -> {
			Articulo articulo = new Articulo();
			articulo.setNombre("nuevo " + LocalDateTime.now());
			articulo.setPrecio(new BigDecimal(1.5));
			entityManager.persist(articulo);
		});
		
		Articulo articulo = JpaHelper.execute(entityManager -> {
			return entityManager.find(Articulo.class, 3L);
		});
		
		show(articulo);
		
		App.getInstance().getEntityManagerFactory().close();
	}
	
	private static void show (Articulo articulo) {
		System.out.printf("%4s %-30s %-30s %s %n",
				articulo.getId(), articulo.getNombre(), articulo.getCategoria(), articulo.getPrecio());
	}
	
}
