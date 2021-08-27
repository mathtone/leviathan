using Leviathan.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLeviathan.ApiSystem.Api {

	[ApiComponent("Api System", "System")]
	public class ApiSystemController : ServiceController<IApiControllersService> {
		public ApiSystemController(IApiControllersService service) :
			base(service) {
		}

		[HttpGet]
		public ActionResult<IEnumerable<ApiControllerListing>> Get() => Service.ControllerTypes()
			.Select(c => new ApiControllerListing {
				Id = c.Id,
				Name = c.ComponentAttributes.OfType<ApiComponentAttribute>().Single().Name,
				TypeName = c.Type.Name,
				Assembly = c.Type.Assembly.GetName().Name
			})
			.ToArray();
	}

	public class ApiControllerListing {
		public int Id { get; init; }
		public string Name { get; init; }
		public string TypeName { get; init; }
		public string Assembly { get; init; }
	}
}