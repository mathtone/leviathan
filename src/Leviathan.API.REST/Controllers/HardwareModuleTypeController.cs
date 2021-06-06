using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leviathan.DataAccess;

namespace Leviathan.API.REST.Controllers {

	[ApiController]
	[Route("[controller]")]
	public class HardwareModuleTypeController : ControllerBase {

		readonly ILogger<HardwareModuleTypeController> logger;
		readonly IListRepository<HardwareModuleTypeInfo, int> moduleTypes;

		public HardwareModuleTypeController(ILogger<HardwareModuleTypeController> logger, IListRepository<HardwareModuleTypeInfo, int> moduleTypes) {

			this.logger = logger;
			this.moduleTypes = moduleTypes;
		}

		[HttpGet("List")]
		public IEnumerable<HardwareModuleTypeInfo> List() => moduleTypes.List();

		[HttpGet("Read")]
		public HardwareModuleTypeInfo Read(int id) => moduleTypes.Read(id);

	}
}