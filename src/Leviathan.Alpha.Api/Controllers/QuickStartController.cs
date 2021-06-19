using Leviathan.Alpha.Api.Controllers.Support;
using Leviathan.Alpha.Components;
using Leviathan.Alpha.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers {

	[ApiController, Route("api/[controller]")]
	public class QuickStartController : ServiceControllerBase<IQuickStartService> {
		public QuickStartController(IQuickStartService service) :
			base(service) {
		}

		[HttpGet]
		public async Task<QuickStartCatalog> Catalog() => await Service.Catalog();
	}
}