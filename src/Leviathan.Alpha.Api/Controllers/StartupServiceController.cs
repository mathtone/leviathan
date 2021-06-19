using Leviathan.Alpha.Configuration;
using Leviathan.Alpha.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leviathan.Alpha.Api.Controllers {

	[ApiController, Route("api/[controller]/[action]")]
	public class StartupServiceController : ControllerBase {

		IStartupService service;

		public StartupServiceController(IStartupService service) {
			this.service = service;
		}

		[HttpGet]
		public StartupServiceCatalog Catalog() => service.Catalog();

		[HttpPost]
		public void SetStartupInfo(StartupInfo startupInfo) => service.SetStartupInfo(startupInfo);

		[HttpPost]
		public void ApplyConfiguration() => service.ApplyStartupConfig();
	}
}