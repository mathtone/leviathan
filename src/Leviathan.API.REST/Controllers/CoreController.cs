using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Leviathan.API.REST.Controllers {

	[ApiController]
	[Route("[controller]")]
	public class CoreController : ControllerBase {

		private readonly ILogger<CoreController> _logger;

		public CoreController(ILogger<CoreController> logger) {
			_logger = logger;
		}

		[HttpGet]
		public void Get() {

		}
	}
}