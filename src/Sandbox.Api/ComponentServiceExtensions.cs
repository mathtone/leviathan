using Leviathan.Services.Sdk;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sandbox.Api {
	public static class ComponentServiceExtensions {
		public static IServiceCollection AddComponentServices(this IServiceCollection services) {
			var path = AppDomain.CurrentDomain.BaseDirectory;
			var files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
			//var apiControllerAssemblies = new List<Assembly>();
			foreach (var f in files) {

				var name = AssemblyName.GetAssemblyName(f);
				var assembly = Assembly.Load(name);
				//var containsApiControllers = false;
				foreach (var t in assembly.GetExportedTypes()) {

					if (t.IsAssignableTo(typeof(ControllerBase))) {
						//containsApiControllers = true;
					}

					var attr = t.GetCustomAttribute<ServiceComponentAttribute>();
					if (attr != null) {
						if (attr is SingletonServiceAttribute) {
							var primary = attr.ServiceTypes.First();
							services.AddSingleton(primary, t);
							foreach (var i in attr.ServiceTypes.Skip(1)) {
								services.AddSingleton(i, svc => svc.GetRequiredService(primary));
							}
						}
						if (attr is TransientServiceAttribute) {

							var primary = attr.ServiceTypes.First();
							services.AddTransient(primary, t);
							foreach (var i in attr.ServiceTypes.Skip(1)) {
								services.AddTransient(i, svc => svc.GetRequiredService(primary));
							}
						}
						if (attr is ScopedServiceAttribute) {
							var primary = attr.ServiceTypes.First();
							services.AddScoped(primary, t);
							foreach (var i in attr.ServiceTypes.Skip(1)) {
								services.AddScoped(i, svc => svc.GetRequiredService(primary));
							}
						}
						//if (attr is HostedServiceAttribute) {
						//	var primary = attr.ServiceTypes.First();
						//	//yeah...
						//	services.AddTransient(primary, t);

						//	foreach (var i in attr.ServiceTypes.Skip(1)) { 
						//		services.AddTransient(i, svc => svc.GetRequiredService(primary));
						//	}
						//}
					}
				}
				//if (containsApiControllers) {
				//	apiControllerAssemblies.Add(assembly);
				//}
			}

			return services;
		}
	}
}
