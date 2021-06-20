using Leviathan.Alpha.Components;
using Leviathan.Components;
using Leviathan.REST;
using Leviathan.SDK;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers.Services {
	[ApiController, Route("api/services/[controller]/[action]")]
	public class ComponentsController : ServiceControllerBase<IComponentsService> {

		public ComponentsController(IComponentsService service) : base(service) { }

		[HttpPost]
		public async Task<ComponentsCatalog> Catalog() => await Service.CatalogAsync();
	}
}