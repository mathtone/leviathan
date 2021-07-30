using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Mvc;

namespace Leviathan.TestService1 {

	//[Route("api/[controller]")]
	[ApiComponent("Test")]
	public class TestService1Controller : ServiceController<ITestService1> {

		public TestService1Controller(ITestService1 service) :
			base(service) {
		}

		[HttpGet]
		public void Test() { }
	}
}