using Microsoft.AspNetCore.Mvc;
using System;

namespace Leviathan.REST {
	public abstract class ServiceControllerBase<SERVICE> : ControllerBase {
		protected SERVICE Service { get; }

		public ServiceControllerBase(SERVICE service) {
			this.Service = service;
		}
	}
}
