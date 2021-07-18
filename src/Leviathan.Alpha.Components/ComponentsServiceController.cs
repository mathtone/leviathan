using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Components {

	[ApiComponent,Route("api/[controller]")]
	public class ComponentsServiceController : ServiceController<IComponentsService> {

		public ComponentsServiceController(IComponentsService service) :
			base(service) {
		}

		[HttpGet, Route("[action]")]
		public Task<ComponentsServiceCatalog> Catalog() => Service.Catalog();
	}
}