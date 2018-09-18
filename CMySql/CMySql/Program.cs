using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;

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
			Console.WriteLine("Número de columnas = " + dataReader.FieldCount);
			for (int index = 0; index < dataReader.FieldCount; index++)
				Console.WriteLine("Columna {0} = {1}", index, dataReader.GetName(index));
			String[] fieldNames = getFieldNames(dataReader);
			for (int index = 0; index < fieldNames.Length; index++)
				Console.WriteLine("Columna {0} = {1}", index, fieldNames[index]);

            //Luis
			foreach (String fieldName in fieldNames)
				Console.WriteLine("Columna = " + fieldName);

			//while (dataReader.Read())
				//Console.WriteLine("id='{0}' nombre='{1}'", dataReader["id"], dataReader["nombre"]);
			//Console.WriteLine("id='{0}' nombre='{1}'", datareader[0], datareader[1]);
			dataReader.Close();

			dbConnection.Close();
		}

		private static String[] getFieldNames (IDataReader dataReader) {
			//int index = 0;
			//String[] fieldNames = new string[dataReader.FieldCount];
			//while (index < dataReader.FieldCount) {
			//	fieldNames[index] = dataReader.GetName(index);
			//	index++;
			//}

            ////Luis
			////String[] fieldNames = new string[dataReader.FieldCount];
			////for (int index = 0; index < dataReader.FieldCount; index++)
			//	//fieldNames[index] = dataReader.GetName(index);

			//return fieldNames;

			List<string> fieldNameList = new List<string>();
			for (int index = 0; index < dataReader.FieldCount; index++)
				fieldNameList.Add(dataReader.GetName(index));
			return fieldNameList.ToArray();
		}
	}
}
