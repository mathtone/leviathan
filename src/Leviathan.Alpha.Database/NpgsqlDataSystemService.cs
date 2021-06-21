//using Leviathan.Alpha.Configuration;
using Leviathan.Alpha.Configuration;
using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Leviathan.Services;
using Npgsql;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Database {
	public interface IDataSystemService<out CN> : IDataSystemService
		where CN : IDbConnection {
		ISystemConnectionService<CN> SystemDB { get; }
		IInstanceConnectionService<CN> InstanceDB { get; }
	}

	public interface ISystemConnectionService<out CN> : IDbConnectionService<CN> where CN : IDbConnection { }
	public interface IInstanceConnectionService<out CN> : IDbConnectionService<CN> where CN : IDbConnection { }
	public interface IDataSystemService : IAsyncInitialize {
		Task<DataSystemCatalog> CatalogAsync();
	}

	public record DataSystemCatalog {

		public DatabaseInfo DatabaseInfo { get; init; }
		public bool MustInitialize { get; init; }
		public bool HostLocated { get; init; }
		public bool? DatabaseLocated { get; init; }
		public bool CredentialsConfigured { get; init; }
		public string DbOwner { get; init; }
		public bool? CredentialsValid { get; init; }
		public string[] ServerDbs { get; init; }
	}

	public record DatabaseInfo {
		public bool Configured { get; init; }
		public string HostName { get; init; }
		public string InstanceDbName { get; init; }
	}

	public record DatabaseConfig {
		public string HostName { get; init; }
		public string InstanceDbName { get; init; }
		public BasicLogin DbCredentials { get; init; }
	}

	public record BasicLogin {
		public string Login { get; init; }
		public string Password { get; init; }
	}

	public class NpgsqlDataSystemService : ServiceComponent, IDataSystemService<NpgsqlConnection> {

		ILeviathanSystem System { get; }
		IConfigManager<DatabaseConfig> ConfigService { get; }

		public ISystemConnectionService<NpgsqlConnection> SystemDB { get; private set; }
		public IInstanceConnectionService<NpgsqlConnection> InstanceDB { get; private set; }

		protected DatabaseConfig CurrentConfig => ConfigService.Config;

		public NpgsqlDataSystemService(ILeviathanSystem system, IConfigManager<DatabaseConfig> config) {
			this.System = system;
			this.ConfigService = config;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {
			await base.InitializeAsync();
			SystemDB = new SystemConnectService(ConnectSysytem);
			InstanceDB = new InstanceConnectService(ConnectInstance);
		}

		public async Task<DataSystemCatalog> CatalogAsync() {
			await Initialize;

			var dbs = await ListServerDatabases();

			return new() {
				CredentialsValid = true,
				HostLocated = true,
				MustInitialize = true,
				DbOwner = CurrentConfig.DbCredentials?.Login,
				CredentialsConfigured = CurrentConfig.DbCredentials != null,
				DatabaseLocated = dbs.Contains(CurrentConfig.InstanceDbName),
				ServerDbs = dbs,
				DatabaseInfo = new() {
					HostName = CurrentConfig.HostName,
					InstanceDbName = CurrentConfig.InstanceDbName,
				}
			};
		}

		async Task<string[]> ListServerDatabases() => await SystemDB.Connect().UsedAsync(c => c
			 .CreateCommand("SELECT datname FROM pg_database")
			 .ExecuteReaderAsync()
			 .ToArrayAsync(r => r.GetString(0))
		);

		NpgsqlConnection ConnectSysytem() => Connect("postgres");
		NpgsqlConnection ConnectInstance() => Connect(CurrentConfig.InstanceDbName);
		NpgsqlConnection Connect(string dbName) => new(DbConnectionString(
			CurrentConfig.HostName,
			CurrentConfig.DbCredentials.Login,
			CurrentConfig.DbCredentials.Password,
			dbName
		));

		static string DbConnectionString(string hostName, string login, string password, string database) =>
			$"Host={hostName};Username={login};Database={database};Password={password};";

	}
}