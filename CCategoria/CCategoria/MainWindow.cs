using MySql.Data.MySqlClient;
using System;
using System.Data;
using Gtk;

using CCategoria;

using System.Reflection;

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
		//for (int index = 0; index < dataReader.FieldCount; index++)
		//treeView.AppendColumn(dataReader.GetName(index), new CellRendererText(), "text", index);

		//Luis
		CellRendererText cellRendererText = new CellRendererText();
    //    treeView.AppendColumn(
    //        "ID",
    //        cellRendererText,
    //        delegate (TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				////Categoria categoria = (Categoria)tree_model.GetValue(iter, 0);
				////cellRendererText.Text = categoria.Id + "";
				//object model = tree_model.GetValue(iter, 0);
				//object value = model.GetType().GetProperty("Id").GetValue(model);
				//cellRendererText.Text = value + "";
    //        }
    //    );

    //    treeView.AppendColumn(
    //        "Nombre",
    //        cellRendererText,
    //        delegate (TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				////Categoria categoria = (Categoria)tree_model.GetValue(iter, 0);
				////cellRendererText.Text = categoria.Nombre + "";
				//object model = tree_model.GetValue(iter, 0);
    //            object value = model.GetType().GetProperty("Nombre").GetValue(model);
				//cellRendererText.Text = value + "";
        //    }
        //);

		string[] properties = new string[] { "Id", "Nombre" };

		foreach(string property in properties) {
			treeView.AppendColumn(
				property,
                cellRendererText,
                delegate (TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
                    //Categoria categoria = (Categoria)tree_model.GetValue(iter, 0);
                    //cellRendererText.Text = categoria.Id + "";
                    object model = tree_model.GetValue(iter, 0);
				object value = model.GetType().GetProperty(property).GetValue(model);
                    cellRendererText.Text = value + "";
                }
            );
		}

		//ListStore listStore = new ListStore(typeof(ulong), typeof(string));
		//ListStore listStore = new ListStore(typeof(string), typeof(string));
		ListStore listStore = new ListStore(typeof(Categoria));
        treeView.Model = listStore;

        //Values
		//Luis
        while (dataReader.Read())
			listStore.AppendValues(new Categoria((ulong)dataReader["id"], (string)dataReader["nombre"]));
  
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
