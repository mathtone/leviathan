using Leviathan.WebApi.Sdk;

namespace TheLeviathan.Components {

	[ApiComponent(ApiModules.Core)]
	public class ComponentsController : ServiceController<IComponentsService> {
		public ComponentsController(IComponentsService service) : base(service) {
		}
	}
}