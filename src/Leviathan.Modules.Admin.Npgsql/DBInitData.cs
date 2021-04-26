using System;
using System.Data;
using Leviathan.DataAccess;
using Leviathan.Utilities;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.Modules.Admin.Npgsql {

	public class DBInitData : DataAccessComponent, IDBInitData {

		public DBInitData(IDbConnectionProvider connectionProvider) :
			base(connectionProvider.CreateConnection()) { }

		public bool LocateDB(string dbName) => Connection.CreateCommand(Queries.LocateDatabase)
			.WithInput("@p0", dbName)
			.ExecuteReadSingle(r => Convert.ToBoolean(r[0]));

		public void DropDB(string dbName) => Connection.CreateCommand(Queries.DropDatabase)
			.WithTemplate("@p0", dbName)
			.ExecuteNonQuery();

		public void CreateDB(string dbName) => Connection.CreateCommand(Queries.CreateDatabase)
			.WithTemplate("@p0", dbName)
			.ExecuteNonQuery();

		public void InitialzeDB(string dbName) => Connection.CreateCommand(Queries.CreateDatabaseObjects)
			.WithTemplate("@p0", dbName)
			.ExecuteNonQuery();

		protected class Queries {
			public static readonly string DropDatabase = LoadLocalResource("Queries.DropDatabase.sqlx");
			public static readonly string CreateDatabase = LoadLocalResource("Queries.CreateDatabase.sqlx");
			public static readonly string CreateDatabaseObjects = LoadLocalResource("Queries.CreateDatabaseObjects.sqlx");
			public static readonly string LocateDatabase = LoadLocalResource("Queries.LocateDatabase.sqlx");
		}
	}
}