using System.Threading.Tasks;
using Leviathan.System.SDK;
using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc;

namespace Leviathan.Alpha.System {
	[ApiComponent]
	public class SystemServiceController : ServiceController<ILeviathanSystem> {

		public SystemServiceController(ILeviathanSystem service) :
			base(service) {
		}

		[HttpGet, Route("[action]")]
		public async Task<SystemServiceCatalog> Catalog() =>
			await Service.Catalog();
	}
}