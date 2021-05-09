using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leviathan.Services.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Leviathan.API.REST.Controllers {

	[ApiController]
	[Route("[controller]")]
	public class CoreController : ControllerBase {

		private readonly ILogger<CoreController> _logger;
		private readonly ILeviathanCore _core;

		public CoreController(ILogger<CoreController> logger, ILeviathanCore core) {
			_logger = logger;
			_core = core;
		}

		[HttpGet]
		public CoreStatus Get() => _core.Status;
	}
}