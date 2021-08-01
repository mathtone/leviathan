using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Leviathan.WebApi.Sdk {
	public class ServiceController<T> : ControllerBase {

		protected T Service { get; }

		public ServiceController(T service) {
			this.Service = service;
		}
	}
	public static class ApiModules {
		public const string Core = "core";
		public const string Hardware = "hardware";
	}


}