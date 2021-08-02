using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox.Api.Controllers {

	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase {

		public TestController(ITestService service) {
			;
		}

		[HttpGet]
		public void Test() {

		}
	}

	[ApiController, Route("api/[controller]")]
	public class ComponentsSystemController<T> {

		[HttpGet]
		public T Test() {
			return default;
		}
	}
}