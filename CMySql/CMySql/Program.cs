using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CMySql
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			IDbConnection dbConnection = new MySqlConnection(
				"server=localhost;database=dbprueba;user=root;password=sistemas;ssl-mode=none"
			);
			dbConnection.Open();

			IDbCommand dbCommand = dbConnection.CreateCommand();
			dbCommand.CommandText = "select * from categoria order by id";
			IDataReader dataReader = dbCommand.ExecuteReader();
			dataReader.
			while (dataReader.Read())
				Console.WriteLine("id='{0}' nombre='{1}'", dataReader["id"], dataReader["nombre"]);
			//Console.WriteLine("id='{0}' nombre='{1}'", datareader[0], datareader[1]);
			dataReader.Close();

			dbConnection.Close();
		}

		private static String[] getFieldNames (IDataReader dataReader) {
            int i = 0
			String[] vs = new string[];
			while (dataReader.GetName)
				vs[i] = dataReader.GetName
		}
	}
}
