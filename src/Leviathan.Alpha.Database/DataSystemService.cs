//using Leviathan.Alpha.Configuration;
using Leviathan.Alpha.Configuration;
using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Leviathan.Services;
using Npgsql;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Database {
	public interface IDataSystemService : IAsyncInitialize {
		Task<DataSystemCatalog> CatalogAsync();
	}

	public record DataSystemCatalog {

		public DatabaseInfo DatabaseInfo { get; init; }
		public bool MustInitialize { get; init; }
		public bool HostLocated { get; init; }
		public bool? DatabaseLocated { get; init; }
		public bool CredentialsConfigured { get; init; }
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

	public class NpgsqlDataSystemService : ServiceComponent, IDataSystemService {

		ILeviathanSystem System { get; }
		IConfigManager<DatabaseConfig> ConfigService { get; }

		protected DatabaseConfig CurrentConfig => ConfigService.Config;

		public NpgsqlDataSystemService(ILeviathanSystem system, IConfigManager<DatabaseConfig> config) {
			this.System = system;
			this.ConfigService = config;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {
			await base.InitializeAsync();
		}

		public async Task<DataSystemCatalog> CatalogAsync() {
			await Initialize;

			var dbs = await ListServerDatabases();

			return new() {
				CredentialsValid = true,
				HostLocated = true,
				MustInitialize = true,
				CredentialsConfigured = CurrentConfig.DbCredentials != null,
				DatabaseLocated = dbs.Contains(CurrentConfig.InstanceDbName),
				ServerDbs = dbs,
				DatabaseInfo = new() {
					HostName = CurrentConfig.HostName,
					InstanceDbName = CurrentConfig.InstanceDbName,

				}
			};
		}

		async Task<string[]> ListServerDatabases() => await ConnectSystem().UsedAsync(c => c
			 .CreateCommand("SELECT datname FROM pg_database")
			 .ExecuteReaderAsync()
			 .ToArrayAsync(r => r.GetString(0))
		);
		NpgsqlConnection ConnectSystem() => Connect("postgres");
		NpgsqlConnection ConnectInstance() => Connect(CurrentConfig.InstanceDbName);
		NpgsqlConnection Connect(string dbName) =>
			new(DbConnectionString(CurrentConfig.HostName, CurrentConfig.DbCredentials.Login, CurrentConfig.DbCredentials.Password, dbName));

		static string DbConnectionString(string hostName, string login, string password, string database) =>
			$"Host={hostName};Username={login};Database={database};Password={password};";
	}
}