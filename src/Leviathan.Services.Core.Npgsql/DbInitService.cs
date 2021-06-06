using System;
using System.Data;
using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Leviathan.Services.Core;
using Npgsql;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.Services.DbInit.Npgsql {

	public class DbInitService : NpgsqlDataService, IDbInitService {

		public DbInitService(string connectionString) :
			base(connectionString) { }

		public bool LocateDatabase(string name) => Immediate(Queries.LocateDatabase, cmd => 
			cmd.WithInput("p0", name).ExecuteReadSingle(r => r.Field<bool>(0))
		);

		public void CreateDatabase(string name) => Connect()
			.Used(cn => {
				cn.CreateCommand(Queries.CreateDatabase).WithTemplate("@p0", name).ExecuteNonQuery();
				cn.ChangeDatabase(name);
				cn.CreateCommand(Queries.CreateDatabaseObjects).ExecuteNonQuery();
			});

		public void VerifyDatabase(string name) {
			if (!LocateDatabase(name))
				CreateDatabase(name);
		}

		protected NpgsqlConnection Connect() => new NpgsqlConnection(connectionString);

		private static class Queries {
			public static readonly string DropDatabase = LoadLocalResource("Queries.Init.DropDatabase.sqlx");
			public static readonly string CreateDatabase = LoadLocalResource("Queries.Init.CreateDatabase.sqlx");
			public static readonly string CreateDatabaseObjects = LoadLocalResource("Queries.Init.CreateDatabaseObjects.sqlx");
			public static readonly string LocateDatabase = LoadLocalResource("Queries.Init.LocateDatabase.sqlx");
		}
	}
} 