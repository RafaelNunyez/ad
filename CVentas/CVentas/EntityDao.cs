using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Serpis.Ad {
	public class EntityDao<TEntity> {
		protected string idPropertyName = "Id";
		protected Type entityType = typeof(TEntity);
		protected List<string> entityPropertyNames = new List<string>();

		public EntityDao() {
			foreach (PropertyInfo propertyinfo in entityType.GetProperties())
				if (propertyinfo.Name == idPropertyName)
					entityPropertyNames.Insert(0, idPropertyName);
				else
					entityPropertyNames.Add(propertyinfo.Name);
		}

		public IEnumerable Enumerable {
			get {
				ArrayList list = new ArrayList();
				IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand();
				string tableName = entityType.Name.ToLower();
				string fieldNamesCsv = string.Join(", ", entityPropertyNames).ToLower();
				string selectSql = string.Format(
					"select {0} from {1} order by {2}",
					fieldNamesCsv, tableName, idPropertyName.ToLower()
				);
				dbCommand.CommandText = selectSql;
				IDataReader dataReader = dbCommand.ExecuteReader();
				while (dataReader.Read()) {
					object entity = Activator.CreateInstance<TEntity>();
					foreach (string propertyName in entityPropertyNames) {
						object value = dataReader[propertyName.ToLower()];
						if (value == DBNull.Value)
							value = null;
						entityType.GetProperty(propertyName).SetValue(entity, value);
					}
					list.Add(entity);
				}
				dataReader.Close();
				return list;
			}
		}

		private static string selectSql = "select * from {0} where {1} = @id";

		public TEntity Load (object id) {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand();
			string tableName = entityType.Name.ToLower();
			dbCommand.CommandText = string.Format(selectSql, tableName, idPropertyName.ToLower());
			DbCommandHelper.AddParameter(dbCommand, "id", id);
            IDataReader dataReader = dbCommand.ExecuteReader();
            dataReader.Read();

			TEntity entity = Activator.CreateInstance<TEntity>();
            foreach (string propertyName in entityPropertyNames) {
                object value = dataReader[propertyName.ToLower()];
				if (value == DBNull.Value)
                    value = null;
                entityType.GetProperty(propertyName).SetValue(entity, value);
            }

			dataReader.Close();
			return entity;
		}

		public void Save (TEntity entity) {
			object id = entityType.GetProperty(idPropertyName).GetValue(entity);
			object defaultId = Activator.CreateInstance(entityType.GetProperty(idPropertyName).PropertyType);
			if (id.Equals(defaultId)) // Id == 0
				insert(entity);
            else
				update(entity);
		}

		protected string insertSql = "insert into {0} ({1}) values ({2})";

		protected void insert (TEntity entity) {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand();

			List<string> fieldsWithoutId = new List<string>();
			List<string> parameters = new List<string>();
			for (int index = 1; index < entityPropertyNames.Count; index++) {
				fieldsWithoutId.Add(entityPropertyNames[index]);
				parameters.Add("@" + entityPropertyNames[index]);
			}


			string tableName = entityType.Name.ToLower();
			
			string fieldNamesCsv = string.Join(", ", fieldsWithoutId).ToLower();

			dbCommand.CommandText = string.Format(insertSql, tableName, fieldNamesCsv, );
            
			dbCommand.ExecuteNonQuery();
		}

		protected string updateSql = "update {0} set {1} where {2} = @id";

		protected void update (TEntity entity) {

			string tableName = entityType.Name.ToLower();

			List<string> fieldParametersPairs = new List<string>();
			for (int index = 1; index < entityPropertyNames.Count; index++) {
				string item = entityPropertyNames[index];
				fieldParametersPairs.Add(item + "=@" + item);
			}
		}

		protected static string deleteSql = "delete from {0} where {1} = @id";

		public void Delete (object id) {
			string tableName = entityType.Name.ToLower();

			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand();
			dbCommand.CommandText = string.Format(deleteSql, tableName, idPropertyName.ToLower());
			DbCommandHelper.AddParameter(dbCommand, "id", id);
            dbCommand.ExecuteNonQuery();
		}
    }
}
