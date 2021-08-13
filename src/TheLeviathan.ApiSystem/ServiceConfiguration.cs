using Leviathan.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace TheLeviathan.ApiSystem {
	public static partial class ServiceConfiguration {
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

		public class ModularControllerRouteConvention : IControllerModelConvention {
			public void Apply(ControllerModel controller) {
				var attr = controller.ControllerType.GetCustomAttribute<ApiComponentAttribute>();
				if (attr != null) {
					controller.Selectors.Add(new SelectorModel {
						AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"api/{attr.ModuleName}/{controller.ControllerName}")),
					});
				}
			}
		}
	}
}