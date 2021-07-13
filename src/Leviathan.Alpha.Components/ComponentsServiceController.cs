using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Components {
	[ApiComponent]
	public class ComponentsServiceController : ServiceController<IComponentsService> {

		public ComponentsServiceController(IComponentsService service) :
			base(service) {
		}

		[HttpGet, Route("[action]")]
		public async Task<ComponentsServiceCatalog> Catalog() =>
			await Service.Catalog();
	}
}