using Leviathan.Alpha.Database;
using Leviathan.REST;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers.Services {
	[ApiController, Route("api/services/[controller]/[action]")]
	public class DataSystemController : ServiceControllerBase<IDataSystemService> {

		public DataSystemController(IDataSystemService service) : base(service) { }

		[HttpPost]
		public async Task<DataSystemCatalog> Catalog() => await Service.CatalogAsync();
	}
}