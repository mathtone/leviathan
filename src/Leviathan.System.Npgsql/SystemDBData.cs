using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Leviathan.SDK;
using Leviathan.Utilities;
using Npgsql;
using System;
using System.Threading.Tasks;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.System.Npgsql {

	public class SystemDBData : ISystemDbData {

		protected ISystemConfiguration Config { get; }
		IDbConnectionProvider<NpgsqlConnection> InstanceDataProvider;
		protected string SystemConnectionString => $"Host={Config.HostName};Username={Config.DbLogin};Database=postgres;Password={Config.DbPassword};Pooling=False";
		protected string InstanceConnectionString => $"Host={Config.HostName};Username={Config.DbLogin};Database={Config.InstanceName};Password={Config.DbPassword};";

		public SystemDBData(ISystemConfiguration config, IDbConnectionProvider<NpgsqlConnection> instanceDataProvider) {
			this.Config = config;
			this.InstanceDataProvider = instanceDataProvider;
		}

		public void DropDB(string dbName) => new NpgsqlConnection(SystemConnectionString).Used(c =>
			c.CreateCommand(DROP).WithTemplate("@p0", dbName).ExecuteNonQuery()
		);

		public void CreateDB(string dbName) {
			new NpgsqlConnection(SystemConnectionString).Used(c => {
				c.CreateCommand(CREATE).WithTemplate("@p0", dbName).ExecuteNonQuery();
			});
			InstanceDataProvider.SetConnectionInfo(InstanceConnectionString);
			//Init
			new NpgsqlConnection(InstanceConnectionString).Used(c => {
				c.CreateCommand(INIT).WithTemplate("@p0", dbName).ExecuteNonQuery();
			});
		}

		public bool LocateDB(string dbName) => new NpgsqlConnection(SystemConnectionString).Used(c =>
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