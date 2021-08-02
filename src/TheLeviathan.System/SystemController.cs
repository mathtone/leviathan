using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Mvc;

namespace TheLeviathan.System {
	[ApiComponent(ApiModules.System)]
	public class SystemController : ServiceController<ISystemService> {

		public SystemController(ISystemService service) :
			base(service) { }

		[HttpGet]
		public void Test() {

		}
	}
}