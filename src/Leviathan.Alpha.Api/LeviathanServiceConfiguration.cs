using Leviathan.Alpha.Npgsql;
using Leviathan.Alpha.Secrets;
using Leviathan.Core;
using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Leviathan.System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Npgsql;

namespace Leviathan.Alpha.Api {
	public class DbConnectionProvider : NpgsqlConnectionProvider, ISystemDbConnectionProvider, IInstanceDbConnectionProvider {
		public DbConnectionProvider(DbServerInfo serverInfo) {
		}

		public NpgsqlConnection Connect() {
			throw new global::System.NotImplementedException();
		}
	}

	public static class LeviathanServiceConfiguration {
		public static IServiceCollection LeviathanWakes(this IServiceCollection services) => services
			.AddSingleton(new DbServerInfo {
				HostName = "poseidonalpha.local",
				Login = "pi",
				Password = "Digital!2021"
			})
			.AddSingleton<ISystemDbConnectionProvider, DbConnectionProvider>()
			.AddSingleton<IInstanceDbConnectionProvider, DbConnectionProvider>()
			.AddSingleton<ISystemConfigService, ConfigurationService>()
			.AddSingleton<ISystemConfigProvider>(svc => svc.GetRequiredService<ISystemConfigService>())
			.AddSingleton<ILeviathanDBService, LeviathanDBService>()
			.AddSingleton<ISystemService, SystemService>()
			.AddSingleton<IStartupService, StartupService>()
			.AddSingleton<ILogger<TheLeviathan>, LogCollector<TheLeviathan>>()
			.AddSingleton<ILeviathan, TheLeviathan>()
			.AddSingleton<ISecrets, SecretsProvider>();

		//.AddSingleton<IDbDataSystem, NpgsqlDBService>();
	}
}
