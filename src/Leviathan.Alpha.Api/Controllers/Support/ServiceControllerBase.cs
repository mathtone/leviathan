using Microsoft.AspNetCore.Mvc;

namespace Leviathan.Alpha.Api.Controllers.Support {
	public class ServiceControllerBase<SVC> : ControllerBase {
		protected SVC Service { get; }

		public ServiceControllerBase(SVC service) {
			this.Service = service;
		}
	}
}