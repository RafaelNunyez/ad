using MySql.Data.MySqlClient;
using System;
using System.Data;
using Gtk;
using System.Reflection;

using CCategoria;
using Serpis.Ad;

public partial class MainWindow : Gtk.Window {

    //private IDbConnection dbConnection;

    public MainWindow() : base(Gtk.WindowType.Toplevel) {
        Build();

		App.Instance.DbConnection = new MySqlConnection(
                "server=localhost;database=dbprueba;user=root;password=sistemas;ssl-mode=none"
        );

		//new CategoriaWindow();

		App.Instance.DbConnection.Open();

		TreeViewHelper.Fill(treeView, new string[] { "Id", "Nombre" }, CategoriaDao.Categorias);
  
  //      //IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand();
  //      //dbCommand.CommandText = "select id, nombre from categoria order by id";
  //      //IDataReader dataReader = dbCommand.ExecuteReader();

		////treeView.AppendColumn("ID", new CellRendererText(), "text", 0);
		////treeView.AppendColumn("Nombre", new CellRendererText(), "text", 1);

		////Columns
		////for (int index = 0; index < dataReader.FieldCount; index++)
		////treeView.AppendColumn(dataReader.GetName(index), new CellRendererText(), "text", index);

		////Luis
		////TreeViewHelper.Fill(treeView, propertyNames, CategoriaDao.List);

  //      CellRendererText cellRendererText = new CellRendererText();
  //      //    treeView.AppendColumn(
  //      //        "ID",
  //      //        cellRendererText,
  //      //        delegate (TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
  //      ////Categoria categoria = (Categoria)tree_model.GetValue(iter, 0);
  //      ////cellRendererText.Text = categoria.Id + "";
  //      //object model = tree_model.GetValue(iter, 0);
  //      //object value = model.GetType().GetProperty("Id").GetValue(model);
  //      //cellRendererText.Text = value + "";
  //      //        }
  //      //    );

  //      //    treeView.AppendColumn(
  //      //        "Nombre",
  //      //        cellRendererText,
  //      //        delegate (TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
  //      ////Categoria categoria = (Categoria)tree_model.GetValue(iter, 0);
  //      ////cellRendererText.Text = categoria.Nombre + "";
  //      //object model = tree_model.GetValue(iter, 0);
  //      //            object value = model.GetType().GetProperty("Nombre").GetValue(model);
  //      //cellRendererText.Text = value + "";
  //      //    }
  //      //);

  //      string[] properties = new string[] { "Id", "Nombre" };

  //      foreach (string property in properties) {
  //          treeView.AppendColumn(
  //              property,
  //              cellRendererText,
  //              delegate (TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
  //                  //Categoria categoria = (Categoria)tree_model.GetValue(iter, 0);
  //                  //cellRendererText.Text = categoria.Id + "";
  //                  object model = tree_model.GetValue(iter, 0);
  //                  object value = model.GetType().GetProperty(property).GetValue(model);
  //                  cellRendererText.Text = value + "";
  //              }
  //          );
  //      }

  //      //ListStore listStore = new ListStore(typeof(ulong), typeof(string));
  //      //ListStore listStore = new ListStore(typeof(string), typeof(string));
  //      ListStore listStore = new ListStore(typeof(Categoria));
  //      treeView.Model = listStore;

		//foreach (Categoria categoria in CategoriaDao.Categorias)
			//listStore.AppendValues(categoria);

        ////Values
        ////Luis
        ////while (dataReader.Read())
        //    //listStore.AppendValues(new Categoria((ulong)dataReader["id"], (string)dataReader["nombre"]));

        ////Manual Values
        ////listStore.AppendValues("1", "cat 1");
        ////listStore.AppendValues("2", "cat 2");

        ////dataReader.Close();
    }

    private void insert() {
        IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand();
        dbCommand.CommandText = "insert into categoria (nombre) values ('categoria 4')";

        dbCommand.ExecuteNonQuery();
    }

    private void update() {
        IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand();
        dbCommand.CommandText = "update categoria set nombre='categoria 4 modificada' where id=4";

        dbCommand.ExecuteNonQuery();
    }

    private void update(Categoria categoria) {
        IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand();
        dbCommand.CommandText = string.Format("update categoria set nombre=@nombre where id=@id");

		//IDbDataParameter dbDataParameterNombre = dbCommand.CreateParameter();
		//dbDataParameterNombre.ParameterName = "nombre";
		//dbDataParameterNombre.Value = categoria.Nombre;
		//dbCommand.Parameters.Add(dbDataParameterNombre);
		DbCommandHelper.AddParameter(dbCommand, "nombre", categoria.Nombre);

        //IDbDataParameter dbDataParameterId = dbCommand.CreateParameter();
        //dbDataParameterId.ParameterName = "id";
        //dbDataParameterId.Value = categoria.Id;
        //dbCommand.Parameters.Add(dbDataParameterId);
		DbCommandHelper.AddParameter(dbCommand, "id", categoria.Id);

        dbCommand.ExecuteNonQuery();
    }

    private void delete() {
        IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand();
        dbCommand.CommandText = "delete from categoria where id=4";

        dbCommand.ExecuteNonQuery();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
		App.Instance.DbConnection.Close();

        Application.Quit();
        a.RetVal = true;
    }
}
