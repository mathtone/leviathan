using Leviathan.System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers {

	[ApiController, Route("api/[controller]/[action]")]
	public class SystemConfigurationController {

		ISystemConfigService Service { get; }

		public SystemConfigurationController(ISystemConfigService service) {
			this.Service = service;
		}

		[HttpGet]
		public SystemConfiguration Get() => Service.Config;

		[HttpPost]
		public async Task Save(SystemConfiguration config) => await Service.SaveAsync(config);

		[HttpPost]
		public async Task<SystemConfiguration> Reload() => await Service.ReloadAsync();
	}

	[ApiController, Route("api/[controller]/[action]")]
	public class SystemController : ControllerBase {

		ISystemService Service { get; }

		public SystemController(ISystemService service) {
			this.Service = service;
		}

		[HttpGet]
		public async Task<SystemStatus> Status() => await Service.GetStatus();
	}
}