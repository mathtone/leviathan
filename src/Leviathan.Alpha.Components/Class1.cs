using Microsoft.AspNetCore.Mvc;
using System;

namespace Leviathan.Alpha.Components {

	[Route("[controller]")]
	public class ComponentsController : ControllerBase {

		[HttpGet]
		public void Test() { }
	}

	public class ServiceController<T> : ControllerBase {

		protected T Service { get; }

		public ServiceController(T service) {
			this.Service = service;
		}
	}
}