using Leviathan.Alpha.Components;
using Leviathan.Alpha.Startup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers {

	[ApiController, Route("api/[controller]/[action]")]
	public class SandboxController : ControllerBase {

		[HttpPost]
		public void Test() { }
	}
	public class ServiceController<SERVICE> : ControllerBase {
		protected SERVICE Service { get; }

		public ServiceController(SERVICE service) {
			this.Service = service;
		}
	}

	[ApiController, Route("api/[controller]/[action]")]
	public class StartupController : ServiceController<IStartupService> {

		public StartupController(IStartupService service) : base(service) { }

		[HttpPost]
		public async Task<StartupCatalog> Catalog() => await Service.CatalogAsync();

		[HttpGet]
		public async Task<IEnumerable<ComponentListing>> Profiles() => await Service.SystemProfiles();

		[HttpPost]
		public async Task ActivateProfile(string name) => await Service.ActivateProfile(name);
	}

	[ApiController, Route("api/[controller]/[action]")]
	public class ComponentsController : ServiceController<IComponentsService> {

		public ComponentsController(IComponentsService service) : base(service) { }

		[HttpPost]
		public async Task<ComponentsCatalog> Catalog() => await Service.CatalogAsync();
	}
}