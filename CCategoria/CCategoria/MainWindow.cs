using MySql.Data.MySqlClient;
using System;
using System.Data;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

		IDbConnection dbConnection = new MySqlConnection(
                "server=localhost;database=dbprueba;user=root;password=sistemas;ssl-mode=none"
            );
        dbConnection.Open();

		IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "select id, nombre from categoria order by id";
        IDataReader dataReader = dbCommand.ExecuteReader();

		//treeView.AppendColumn("ID", new CellRendererText(), "text", 0);
        //treeView.AppendColumn("Nombre", new CellRendererText(), "text", 1);

		//Columns
        for (int index = 0; index < dataReader.FieldCount; index++)
            treeView.AppendColumn(dataReader.GetName(index), new CellRendererText(), "text", index);

		//ListStore listStore = new ListStore(typeof(ulong), typeof(string));
		ListStore listStore = new ListStore(typeof(string), typeof(string));
        treeView.Model = listStore;

        //Values
		//Luis
		while (dataReader.Read())
			for (int index = 0; index < dataReader.FieldCount; index++)
				listStore.AppendValues(dataReader[index] + "", dataReader[index + 1] + "");

		//while (dataReader.Read())
			//listStore.AppendValues(dataReader["id"] + "", dataReader["nombre"] + "");
  
        //Manual Values
		//listStore.AppendValues("1", "cat 1");
		//listStore.AppendValues("2", "cat 2");

		dataReader.Close();

        dbConnection.Close();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}
