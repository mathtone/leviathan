using Leviathan.Alpha.Api.Controllers.Support;
using Leviathan.Alpha.FactoryReset;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers {

	[ApiController, Route("api/[controller]/[action]")]
	public class FactoryResetServiceController : ServiceControllerBase<IFactoryResetService> {

		public FactoryResetServiceController(IFactoryResetService service) :
			base(service) {
		}

		[HttpGet]
		public async Task<DestructCode> BeginSelfDestruct() => await Service.BeginSelfDestruct();

		[HttpPost]
		public async Task FactoryReset(string destructCode) => await Service.FactoryReset(destructCode);
	}
}