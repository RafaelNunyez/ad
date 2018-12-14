package serpis.ad;

import java.util.List;

public class CategoriaConsole {
	
	//Hecho
	public static long getId() {
		return ScannerHelper.getInt("¿Id? ");
	}
	
	public static void newCategoria(Categoria categoria) {
		
	}
		
	public static void editCategoria(Categoria categoria) {
		
	}
	
	public static void idNotExists() {
		System.out.println("No se ha podido encontrar categoría con ese Id.");
	}
	
	public static boolean deleteConfirm() {
		return ScannerHelper.getConfirm("¿Estas seguro que quieres eliminar el registro? (s/N)").equalsIgnoreCase("s");
	}
	
	public static void show(Categoria categoria) {
		System.out.printf("%4s %s %n", categoria.getId(), categoria.getNombre());
	}
	
	public static void showList(List<Categoria> categorias) {
		for (Categoria categoria : categorias)
			System.out.printf("%4s %s %n", categoria.getId(), categoria.getNombre());
	}
	
}
