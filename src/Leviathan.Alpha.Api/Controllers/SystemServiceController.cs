using Leviathan.System.SDK;
using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers {

	[Route("api/[controller]")]
	[ApiController]
	public class SystemServiceController : ServiceController<ILeviathanSystem> {

		public SystemServiceController(ILeviathanSystem service) :
			base(service) {
		}

		[HttpGet, Route("[action]")]
		public async Task<SystemServiceCatalog> Catalog() =>
			await Service.Catalog();
	}
}