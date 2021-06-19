using Leviathan.Alpha.Api.Controllers.Support;
using Leviathan.Alpha.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Leviathan.Alpha.Api.Controllers {
	[ApiController, Route("api/[controller]/[action]")]
	public class LeviathanComponentsController : ServiceControllerBase<ILeviathanComponentsService> {

		public LeviathanComponentsController(ILeviathanComponentsService service) :
			base(service) {
			;
		}

		[HttpGet]
		public IEnumerable<ComponentListing> ComponentTypes() => Service.GetAllComponentTypes();
	}
}