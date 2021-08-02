using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheLeviathan.OneWire {

	[ApiComponent(ApiModules.Channels)]
	public class OneWireController : ServiceController<IOneWireService> {
		public OneWireController(IOneWireService service) : base(service) {

		}

		[HttpGet("[action]")]
		public Task<IEnumerable<string>> BusIds() => Service.BusIdsAsync();

		[HttpGet("[action]")]
		public async Task<IEnumerable<OneWireDeviceListing>> Devices() => await Task.FromResult(Service.ListDevices());
	}
}