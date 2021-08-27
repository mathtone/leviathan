using Leviathan.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLeviathan.ServiceSystem.Api {

	[ApiComponent("Service System","System")]
	public class ServiceSystemController : ControllerBase {

		[HttpGet]
		public void Get() { }
	}
} 