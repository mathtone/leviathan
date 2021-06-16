using Leviathan.Services;
using Leviathan.System;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Leviathan.Alpha {
	public class ConfigurationService : LeviathanServiceBase, ISystemConfigService {

		public SystemConfiguration Config { get; protected set; }
		protected static string LocalPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		protected static string ConfigFileName => $"{LocalPath}{Path.DirectorySeparatorChar}Leviathan.settings.json";

		public ConfigurationService() {
			Initialization = InitializeAsync();
		}

		private async Task InitializeAsync() {
			this.Config = JsonConvert.DeserializeObject<SystemConfiguration>(await File.ReadAllTextAsync(ConfigFileName));
		}

		public async Task SaveAsync(SystemConfiguration config) =>
			await File.WriteAllTextAsync(ConfigFileName, JsonConvert.SerializeObject(config));

		public async Task<SystemConfiguration> ReloadAsync() =>
			this.Config = JsonConvert.DeserializeObject<SystemConfiguration>(
				await File.ReadAllTextAsync(ConfigFileName)
			);
	}
}