using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Leviathan.DataAccess;
using Leviathan.Services.Core.Hardware;

namespace Leviathan.API.REST.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class HardwareModuleController : LeviathanDataController<IHardwareModuleData> {

		ILogger<HardwareModuleController> logger;

		public HardwareModuleController(ILogger<HardwareModuleController> logger, IHardwareModuleData data) : base(data) {
			this.logger = logger;
		}

		[HttpGet("List")]
		public IEnumerable<HardwareModuleInfo> List() => Data.List();

		[HttpGet("Read")]
		public HardwareModuleInfo Read(int id) => Data.Read(id);

	}
}