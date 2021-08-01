using Leviathan.Services.Sdk;
using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace TheLeviathan.Components {

	public static class ServiceConfiguration {

		public static IServiceCollection AddComponentServices(this IServiceCollection services) {

			var path = AppDomain.CurrentDomain.BaseDirectory;
			var files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);

			foreach (var f in files) {

				var name = AssemblyName.GetAssemblyName(f);
				var assembly = Assembly.Load(name);

				foreach (var t in assembly.GetExportedTypes()) {

					var attr = t.GetCustomAttribute<ServiceComponentAttribute>();

					if (attr != null) {

						var primary = attr.PrimaryServiceType;
						if (attr is SingletonServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, t, services.AddSingleton, services.AddSingleton);

						if (attr is TransientServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, t, services.AddTransient, services.AddTransient);

						if (attr is ScopedServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, t, services.AddScoped, services.AddScoped);

						if (attr is HostedSingletonServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, t, services.AddHostedSingleton, services.AddSingleton);
					}
				}

				static void RegisterService(Type primary, Type[] secondary, Type implementation, Func<Type, Type, IServiceCollection> registerAction, Func<Type, object> secondaryRegisterAction) {
					registerAction(primary, implementation);
					if (secondary != null) {
						foreach (var i in secondary) {
							secondaryRegisterAction(i);
						}
					}
				}
			}

			services.AddControllers(o => o.Conventions.Add(new ModularControllerRouteConvention()));
			return services;
		}
	}

	public class ModularControllerRouteConvention : IControllerModelConvention {
		public void Apply(ControllerModel controller) {
			var type = controller.ControllerType;
			var attr = type.GetCustomAttribute<ApiComponentAttribute>();
			if (attr != null) {
				controller.Selectors.Add(new SelectorModel {
					AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"api/{attr.ModuleName}/{controller.ControllerName}")),
				});
			}
		}
	}
}