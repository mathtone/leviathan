using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace TheLeviathan.ApiSystem {
	public static class ServiceConfiguration {
		public static IMvcBuilder ConfigureMvc(this IServiceCollection svcCollection) {

			svcCollection.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.Api", Version = "v1" });
				c.EnableAnnotations();
			});
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
	}
}
