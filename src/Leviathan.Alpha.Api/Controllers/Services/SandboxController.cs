using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Leviathan.Alpha.Api.Controllers.Services {

	[ApiController, Route("api/services/[controller]/[action]")]
	public class SandboxController : ControllerBase {

		[HttpPost]
		public void Test() { }
	}

	
}