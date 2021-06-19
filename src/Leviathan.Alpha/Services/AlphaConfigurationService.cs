using Leviathan.Alpha.Configuration;
using Leviathan.System;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Services {


	public class AlphaSystemConfigurationService : ISystemConfigService<AlphaSystemConfiguration> {
		public Task Initialize { get; }
		public AlphaSystemConfiguration Config { get; protected set; }
		protected static string LocalPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		protected static string ConfigFileName => $"{LocalPath}{Path.DirectorySeparatorChar}Leviathan.settings.json";

		public AlphaSystemConfigurationService() {
			this.Config = JsonConvert.DeserializeObject<AlphaSystemConfiguration>(File.ReadAllText(ConfigFileName));
			this.Initialize = InitializeAsync();
		}

		private async Task InitializeAsync() {
			await Task.CompletedTask;
			//this.Config = JsonConvert.DeserializeObject<AlphaSystemConfiguration>(await File.ReadAllTextAsync(ConfigFileName));
		}

		public async Task SaveAsync(AlphaSystemConfiguration config) {
			await File.WriteAllTextAsync(ConfigFileName, JsonConvert.SerializeObject(config));
			await ReloadAsync();
		}

		public async Task<AlphaSystemConfiguration> ReloadAsync() =>
			this.Config = JsonConvert.DeserializeObject<AlphaSystemConfiguration>(
				await File.ReadAllTextAsync(ConfigFileName)
			);
	}

}