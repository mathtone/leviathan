using Leviathan.Alpha.Components;
using Leviathan.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Services {
	public interface IQuickStartService : IAsyncInitialize {
		Task<QuickStartCatalog> Catalog();
	}

	public class QuickStartService : IQuickStartService {
		ILeviathanComponentsService Components { get; }

		public Task Initialize { get; }

		public QuickStartService(ILeviathanComponentsService components) {
			this.Components = components;
			this.Initialize = InitializeAsync();
		}

		public async Task<QuickStartCatalog> Catalog() {
			await Initialize;
			return new QuickStartCatalog {
				Profiles = this.Components.GetAllComponentTypes()
					.Where(c => c.Category == "Quick start profile")
					.Select(c => new ConfigProfileListing {
						ProfileName = c.Name,
						Description = c.Description
					})
			};
			
		}

		private async Task InitializeAsync() => await Task.CompletedTask;

		public async Task ApplyProfile(string profileId) {
			await Task.CompletedTask;
		}
	}


	[LeviathanComponent("Robo-Tank", "Quick start profile", "Configures the leviathan to run on the Robo-Tank controller")]
	public class RoboTankProfile : QuickStartProfile {

	}

	[LeviathanComponent("Basic", "Quick start profile", "Configures the leviathan to run with factory settings")]
	public class BasicProfile : QuickStartProfile {

	}

	[LeviathanComponent("Hardcore Mode", "Quick start profile", "Do nothing. Leave me alone.  I will awaken my own leviathan.")]
	public class HardcoreProfile : QuickStartProfile {

	}

	public abstract class QuickStartProfile {
	}

	public class ConfigProfileListing {
		public string ProfileId { get; init; }
		public string ProfileName { get; init; }
		public string Description { get; init; }

	}

	public class QuickStartCatalog {
		public IEnumerable<ConfigProfileListing> Profiles { get; set; }
	}
}