using DbUp;
using Leviathan.SystemHost.Support;
using Microsoft.Extensions.Configuration;

namespace Leviathan.SystemHost.Updater {
	public class UpgradeService {
		
		private readonly NpgSqlConnectionProviderConfig _config;

		public UpgradeService(NpgSqlConnectionProviderConfig config) =>
			_config = config;

		public Task Update() {
			
			//Check for System Config and Generate if not found


			//Check for Keys and Generate if not found

			//Upgrade Database
			DeployChanges.To
				.PostgresqlDatabase(_config.ConnectionStrings["default"])
				.WithScriptsEmbeddedInAssembly(typeof(UpgradeService).Assembly,new DbUp.Engine.SqlScriptOptions() {
					
				})
				.JournalToPostgresqlTable("public", "dbup_journal")
				.LogToConsole()
				.Build()
				.PerformUpgrade();

			return Task.CompletedTask;
		}
	}
}