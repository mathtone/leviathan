using Leviathan.Alpha.Configuration;
using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Leviathan.System;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.Alpha.Npgsql {
	public interface IDataSystemService<out CN> : IDataSystemService where CN : IDbConnection {
		CN SystemConnection();
		CN InstanceConnection();
	}

	public interface IDataSystemService {
		Task ReInitialize();
		Task<DataSystemServiceCatalog> Catalog();
		event EventHandler InitializeComplete;
	}

	public class DataSystemServiceCatalog {
		public bool InstanceDatabaseFound { get; init; }
		public string[] DatabasesFound { get; init; }
	}

	public class DataSystemService : IDataSystemService<NpgsqlConnection> {

		public event EventHandler InitializeComplete;

		protected ISystemConfigService<AlphaSystemConfiguration> ConfigService { get; }
		protected IDbConnectionProvider<NpgsqlConnection, string> Connections { get; }
		public Task Initialize { get; private set; }
		public DataSystemServiceCatalog CurrentCatalog { get; protected set; }
		protected AlphaSystemConfiguration CurrentConfig => ConfigService?.Config;
		protected DatabaseInfo DbInfo => CurrentConfig?.StartupInfo.DatabaseInfo;

		public NpgsqlConnection SystemConnection() => Connections.Get(DbConnectionString(DbInfo, "postgres"));
		public NpgsqlConnection InstanceConnection() => Connections.Get(DbConnectionString(DbInfo, DbInfo.InstanceDbName));

		public DataSystemService(ISystemConfigService<AlphaSystemConfiguration> configService, IDbConnectionProvider<NpgsqlConnection, string> connections) {
			this.ConfigService = configService;
			this.Connections = connections;
			this.Initialize = InitializeAsync();
		}

		private async Task InitializeAsync() {
			await ConfigService.Initialize;
			CurrentCatalog = await SystemConnection().UsedAsync(async c =>
				new DataSystemServiceCatalog {
					DatabasesFound = await c.CreateCommand("SELECT datname FROM pg_database;")
						.ExecuteReaderAsync()
						.ToArrayAsync(r => r.GetString(0)),
					InstanceDatabaseFound = await c.CreateCommand("SELECT datname FROM pg_database WHERE LOWER(datname) = @dbname;")
						.WithInput("@dbname", DbInfo.InstanceDbName.ToLower())
						.ExecuteReaderAsync()
						.ConsumeAsync(r => true)
						.AnyAsync()
				}
			);
			(_ = InitializeComplete)?.Invoke(this, new EventArgs());
		}

		public async Task<DataSystemServiceCatalog> Catalog() =>
			await WhenInitialized(() => CurrentCatalog);

		public async Task ReInitialize() {
			Initialize = InitializeAsync();
			await Initialize;
			
			if (CurrentCatalog.InstanceDatabaseFound) {
				//drop & re-create
				throw new Exception("Database exists, must perform factory reset");
			}

			//create DB
			await SystemConnection().UsedAsync(async c => await c
				.CreateCommand(CREATE)
				.WithTemplate("@:db_name", DbInfo.InstanceDbName)
				.ExecuteNonQueryAsync()
			);

			this.Initialize = InitializeAsync();
		}

		async Task<T> WhenInitialized<T>(Func<T> function) {
			await Initialize;
			return function();
		}

		async Task WhenInitialized(Action action) {
			await Initialize;
			action();
		}

		static string DbConnectionString(DatabaseInfo dbInfo, string database) =>
			$"Host={dbInfo.HostName};Username={dbInfo.DbServerCredentials.Login};Database={database};Password={dbInfo.DbServerCredentials.Password};";

		static readonly string LOCATE = LoadLocal("Queries.LocateDB.sqlx");
		static readonly string CREATE = LoadLocal("Queries.CreateDB.sqlx");
		static readonly string INIT = LoadLocal("Queries.InitDB.sqlx");
	}
}