using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leviathan.Services.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Leviathan.API.REST.Controllers {

	public class CoreStatusReport {
		public CoreStatus Status { get; init; }
		public IEnumerable<string> Messages { get; init; }
	}

	[ApiController]
	[Route("[controller]")]
	public class CoreController : ControllerBase {

		private readonly ILogger<CoreController> _logger;
		private readonly ILeviathanCore _core;

		public CoreController(ILogger<CoreController> logger, ILeviathanCore core) {
			_logger = logger;
			_core = core;
		}

		[HttpGet("GetStatus")]
		public CoreStatusReport GetStatus() => new() {
			Status = _core.Status
		};
	}
}