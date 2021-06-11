using Leviathan.Hardware;
using Leviathan.Initialization;
using Leviathan.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Rest.Api.Controllers.Core {

	[ApiController]
	[Route("Service/[controller]/[action]")]
	public class InitializationController : ServiceController<IInitializationService> {

		public InitializationController(IInitializationService service) : base(service) { }

		[HttpPost]
		public void FactoryReset() => Service.FactoryReset();

		[HttpPost]
		public void ApplyProfile(string configName) => Service.ApplyProfile(configName);

		[HttpGet]
		public IEnumerable<ConfigurationProfileListing> ListProfiles() => Service.ListProfiles();

		[HttpPost]
		public void CompleteInitialization() => Service.CompleteInitialization();

		[HttpPost]
		public void Configure(SystemConfiguration config) => Service.Configure(config);

		[HttpGet]
		public SystemConfiguration CurrentConfig() => Service.CurrentConfig();
	}
}