using Leviathan.Alpha.Components;
using Leviathan.Alpha.Startup;
using Leviathan.Components;
using Leviathan.REST;
using Leviathan.SDK;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers.Services {
	[ApiController, Route("api/services/[controller]/[action]")]
	public class StartupController : ServiceControllerBase<IStartupService> {

		public StartupController(IStartupService service) : base(service) { }

		[HttpPost]
		public async Task<StartupCatalog> Catalog() => await Service.CatalogAsync();

		[HttpGet]
		public async Task<IEnumerable<ComponentListing>> Profiles() => await Service.SystemProfiles();

		[HttpPost]
		public async Task ActivateProfile(string name) => await Service.ActivateProfile(name);

		//[HttpPost]
		//public async Task FactoryReset() => await Service.FactoryReset();
	}
}