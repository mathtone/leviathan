using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheLeviathan.OneWire {

	[ApiComponent(ApiModules.Hardware)]
	public class OneWireController : ServiceController<IOneWireService> {
		public OneWireController(IOneWireService service) : base(service) {

		}

		[HttpGet("[action]")]
		public Task<IEnumerable<string>> BusIds() => Service.BusIdsAsync();
	}
}