using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leviathan.DataAccess;
using Leviathan.Services.Core.Hardware;

namespace Leviathan.API.REST.Controllers {

	[ApiController]
	[Route("[controller]")]
	public class HardwareModuleTypeController : LeviathanDataController<IHardwareModuleTypeData> {

		readonly ILogger<HardwareModuleTypeController> logger;

		public HardwareModuleTypeController(ILogger<HardwareModuleTypeController> logger, IHardwareModuleTypeData data) : base(data) {
			this.logger = logger;
		}

		[HttpGet("List")]
		public IEnumerable<HardwareModuleTypeInfo> List() => Data.List();

		[HttpGet("Read")]
		public HardwareModuleTypeInfo Read(int id) => Data.Read(id);

	}
}