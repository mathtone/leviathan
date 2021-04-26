using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leviathan.DataAccess;
using Npgsql;
using static Leviathan.Utilities.ResourceLoader;
namespace Leviathan.DB.Npgsql {

	public class InitializationSettings {
		//public string InstanceName { get; set; }\
		public int InstanceId { get; set; }
		public string DbName { get; set; }
		public bool DropDb { get; set; }
		public bool CreateDb { get; set; }
		public bool CreateObjects { get; set; }
		public string HostName { get; set; }
	}

	public class NpgSqlDataService {
		protected NpgsqlConnection Connection { get; set; }

		public NpgSqlDataService(NpgsqlConnection connection) {

		}

		protected NpgsqlCommand CreateCommand(string commandText) => new(commandText) {
			Connection = this.Connection
		};
	}

	public class InitData : NpgSqlDataService {

		public InitData(NpgsqlConnection connection) : base(connection) { }

		public bool LocateDatabase(string name) =>
			CreateCommand(Queries.LocateDatabase)
				.WithInput("@p0", name)
				.ExecuteReadSingle(r => r.Field<bool>(0));

		public void InitDb(InitializationSettings settings) {

			if (settings.DropDb) {
				CreateCommand(Queries.DropDatabase)
					.WithTemplate("@p0", settings.DbName)
					.ExecuteNonQuery();
			}

			if (settings.CreateDb) {
				CreateCommand(Queries.CreateDatabase)
					.WithTemplate("@p0", settings.DbName)
					.ExecuteNonQuery();
			}

			this.Connection.ChangeDatabase(settings.DbName);

			if (settings.CreateObjects) {
				CreateCommand(Queries.CreateDatabaseObjects)
					.ExecuteNonQuery();
			}
		}

		private static class Queries {
			public static readonly string DropDatabase = LoadLocalResource("Queries.Admin.DropDatabase.sqlx");
			public static readonly string CreateDatabase = LoadLocalResource("Queries.Admin.CreateDatabase.sqlx");
			public static readonly string CreateDatabaseObjects = LoadLocalResource("Queries.Admin.CreateDatabaseObjects.sqlx");
			public static readonly string LocateDatabase = LoadLocalResource("Queries.Admin.LocateDatabase.sqlx");
		}
	}
}