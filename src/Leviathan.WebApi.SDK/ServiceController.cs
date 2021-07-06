using Microsoft.AspNetCore.Mvc;

namespace Leviathan.WebApi.SDK {



	public class ServiceController<T> : ControllerBase {

		protected T Service { get; }

		public ServiceController(T service) {
			this.Service = service;
		}
	}
}