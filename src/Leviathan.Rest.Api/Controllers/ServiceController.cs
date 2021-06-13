using Microsoft.AspNetCore.Mvc;

namespace Leviathan.Rest.Api.Controllers {
	public abstract class ServiceController<SVC> : ControllerBase {
		protected SVC Service { get; }

		public ServiceController(SVC service) {
			this.Service = service;
		}
	}
}