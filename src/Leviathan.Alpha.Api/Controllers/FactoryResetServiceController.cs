using Leviathan.Alpha.FactoryReset;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers {
	[ApiController, Route("api/[controller]/[action]")]
	public class FactoryResetServiceController : ControllerBase {

		IFactoryResetService service;

		public FactoryResetServiceController(IFactoryResetService service) {
			this.service = service;
		}

		[HttpGet]
		public async Task<DestructCode> BeginSelfDestruct() => await service.BeginSelfDestruct();

		[HttpPost]
		public async Task FactoryReset(string destructCode) => await service.FactoryReset(destructCode);
	}
}