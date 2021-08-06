using Leviathan.Services.Sdk;
using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace TheLeviathan.ApiSystem {
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
			}

			services.ConfigureMvc();
			return services;
		}

		static void RegisterService(Type primary, Type[] secondary, Type implementation, Func<Type, Type, IServiceCollection> registerAction, Func<Type, object> secondaryRegisterAction) {
			registerAction(primary, implementation);
			if (secondary != null) {
				foreach (var i in secondary) {
					secondaryRegisterAction(i);
				}
			}
		}

		public static IMvcBuilder ConfigureMvc(this IServiceCollection svcCollection) {

			svcCollection.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.Api", Version = "v1" }));
			var builder = svcCollection.AddAuthorization()
				.AddControllers(o => o.Conventions.Add(new ModularControllerRouteConvention()))
				.AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

			var services = svcCollection.BuildServiceProvider();
			var featureProvider = new ApiControllerFeatureProvider(services.GetRequiredService<IApiControllersService>());

			return builder.ConfigureApplicationPartManager(apm => {
				var features = apm.FeatureProviders.OfType<ControllerFeatureProvider>().ToArray();
				foreach (var original in features) {
					apm.FeatureProviders.Remove(original);
				}
				apm.FeatureProviders.Add(featureProvider);
			});
		}


		public class ApiControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature> {

			IApiControllersService _controllers;

			public ApiControllerFeatureProvider(IApiControllersService controllers) {
				_controllers = controllers;
			}

			public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) {
				foreach (var type in _controllers.ControllerTypes()) {
					feature.Controllers.Add(type.GetTypeInfo());
				}
			}
		}
	}
}