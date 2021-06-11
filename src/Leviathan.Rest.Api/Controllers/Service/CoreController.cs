using Leviathan.Core;
using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Rest.Api.Controllers.Core {

	[ApiController]
	[Route("Service/[controller]/[action]")]
	public class CoreController : ControllerBase {

		ILeviathanCore Core { get; }

		public CoreController(ILeviathanCore core) {
			this.Core = core;
		}

		[HttpPost]
		public void Start() => Core.Start();

		[HttpPost]
		public void Stop() => Core.Stop();

		[HttpPost]
		public void Restart() => Core.Restart();

		[HttpGet]
		public CoreStatus GetStatus() => Core.GetStatus();
	}
}