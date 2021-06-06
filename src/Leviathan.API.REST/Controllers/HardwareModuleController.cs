using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Leviathan.DataAccess;

namespace Leviathan.API.REST.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class HardwareModuleController : ControllerBase {

		ILogger<HardwareModuleController> logger;
		IListRepository<HardwareModuleInfo, int> modules;

		public HardwareModuleController(ILogger<HardwareModuleController> logger, IListRepository<HardwareModuleInfo, int> modules) {

			this.logger = logger;
			this.modules = modules;
		}

		[HttpGet("List")]
		public IEnumerable<HardwareModuleInfo> List() => modules.List();

		[HttpGet("Read")]
		public HardwareModuleInfo Read(int id) => modules.Read(id);

	}
}