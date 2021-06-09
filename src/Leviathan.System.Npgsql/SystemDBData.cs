using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Leviathan.Utilities;
using Npgsql;
using System;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.System.Npgsql {

	public class SystemDBData : ISystemDbData {

		protected IDbConnectionProvider<NpgsqlConnection> Connections { get; }

		public SystemDBData(IDbConnectionProvider<NpgsqlConnection> connectionProvider) {
			this.Connections = connectionProvider;
		}

		public void DropDB(string dbName) => Connections.Connect().Used(c =>
			c.CreateCommand(DROP).WithTemplate("@p0", dbName).ExecuteNonQuery()
		);

		public void CreateDB(string dbName) => Connections.Connect().Used(c => {
			c.CreateCommand(CREATE).WithTemplate("@p0", dbName).ExecuteNonQuery();
			c.ChangeDatabase(dbName);
			c.CreateCommand(INIT).ExecuteNonQuery();
		});

		public bool LocateDB(string dbName) => Connections.Connect().Used(c =>
			c.CreateCommand(LOCATE)
			.WithInput("@p0", dbName)
			.ExecuteReadSingle(r => r.GetBoolean(0))
		);

		static readonly string LOCATE = LoadLocal("Queries.LocateDB.sqlx");
		static readonly string DROP = LoadLocal("Queries.DropDB.sqlx");
		static readonly string CREATE = LoadLocal("Queries.CreateDB.sqlx");
		static readonly string INIT = LoadLocal("Queries.InitDB.sqlx");
	}
}