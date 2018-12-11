package serpis.ad;

import java.sql.DriverManager;
import java.sql.SQLException;

public class CategoriaMain {
	public static void main(String[] args) throws SQLException {
		App.getInstance().setConnection(
				DriverManager.getConnection("jdbc:mysql://localhost/dbprueba", "root", "sistemas")
			);
		
		Menu.create("Menú Categoria")
		.exitWhen("\t0 - Salir")
		.add("\t1 - Nuevo", CategoriaMain::nuevo)
		.add("\t2 - Editar", CategoriaMain::editar)
		.add("\t3 - Insertar", CategoriaMain::insertar)
		.loop();
		App.getInstance().getConnection().close();
	}
	
	public static void nuevo() {
		System.out.println("Método nuevo");
	}
	
	public static void editar() {
		System.out.println("Método editar");
		int id = ScannerHelper.getInt("Id: ");
	}
	
	public static void insertar() {
		System.out.println("Metodo insertar");
	}
}
