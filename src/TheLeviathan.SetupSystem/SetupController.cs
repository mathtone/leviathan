using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Mvc;

namespace TheLeviathan.SetupSystem {
	[ApiComponent(ApiModules.System)]
	public class SetupController {

		[HttpGet]
		public void Test() { }

	}
}