package serpis.ad;

public class CategoriaMain {
	private static boolean exit = false;
	public static void main(String[] args) {
		Menu.create("Menú Categoria")
		.exitWhen("\t0 - Salir")
		.add("\t1 - Nuevo", CategoriaMain::nuevo)
		.add("\t2 - Editar", CategoriaMain::editar)
		.loop();
	}
	
	public static void nuevo() {
		System.out.println("Método nuevo");
	}
	
	public static void editar() {
		System.out.println("Método editar");
		int id = ScannerHelper.getInt("Id: ");
	}
}
