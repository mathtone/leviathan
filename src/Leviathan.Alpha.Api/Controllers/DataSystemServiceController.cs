using Leviathan.Alpha.Npgsql;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers {
	[ApiController, Route("api/[controller]/[action]")]
	public class DataSystemServiceController : ControllerBase {

		IDataSystemService service;

		public DataSystemServiceController(IDataSystemService service) {
			this.service = service;
		}

		[HttpGet]
		public async Task<DataSystemServiceCatalog> Catalog() => await service.Catalog();

		[HttpPost]
		public async Task Reinitialize() => await service.ReInitialize();
	}
}