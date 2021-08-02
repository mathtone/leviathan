using Leviathan.Services.Sdk;
using Leviathan.WebApi.Sdk;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

namespace TheLeviathan.Components {

	public static class ServiceConfiguration {

		public static IServiceCollection AddComponentServices(this IServiceCollection services) {

			var path = AppDomain.CurrentDomain.BaseDirectory;
			var files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);

			foreach (var f in files) {

				var name = AssemblyName.GetAssemblyName(f);
				var assembly = Assembly.Load(name);

				foreach (var type in assembly.GetExportedTypes()) {

					var attr = type.GetCustomAttribute<ServiceComponentAttribute>();

					if (attr != null) {

						var primary = attr.PrimaryServiceType;
						if (attr is SingletonServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, type, services.AddSingleton, services.AddSingleton);

						if (attr is TransientServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, type, services.AddTransient, services.AddTransient);

						if (attr is ScopedServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, type, services.AddScoped, services.AddScoped);

						if (attr is HostedSingletonServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, type, services.AddHostedSingleton, services.AddSingleton);
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

			services
				.AddControllers(o =>o.Conventions.Add(new ModularControllerRouteConvention()))
				.AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

			return services;
		}
	}
}