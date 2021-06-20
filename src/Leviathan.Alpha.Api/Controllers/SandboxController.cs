using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Leviathan.Alpha.Api.Controllers {

	[ApiController, Route("api/[controller]/[action]")]
	public class SandboxController : ControllerBase {

		[HttpPost]
		public void Test() { }
	}

	
}