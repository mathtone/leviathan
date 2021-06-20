using Leviathan.Components;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leviathan.SDK {
	public interface IStartupService {
		Task<StartupCatalog> CatalogAsync();
		Task<IEnumerable<ComponentListing>> SystemProfiles();
		Task ActivateProfile(string profileTypeName);
		//Task FactoryReset();
		//void SetAdminCredentials(CredentialsConfig credentials);
	}

	public class StartupCatalog {

		public IHostEnvironment Environment { get; init; }
	}


	

	
}
