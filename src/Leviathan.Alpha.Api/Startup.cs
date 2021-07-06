using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;
using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Leviathan.Alpha.Api {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {

			services.AddTheLeviathan();

			services
				.AddControllers()
				//.AddControllers(o => o.Conventions.Add(new ModularControllerRouteConvention()))
				//.ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new ModularControllerFeatureProvider()))
				.AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.Alpha.Api", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leviathan.Alpha.Api v1"));
			}

			app.AwakenTheLeviathan(env, lifetime);

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}

	//public class ModularControllerRouteConvention : IControllerModelConvention {
	//	public void Apply(ControllerModel controller) {
	//		//var attr = controller.ControllerType.GetCustomAttribute<ApiComponentAttribute>();
	//		//if (attr != null) {
	//		//	controller.Selectors.Add(new SelectorModel {
	//		//		AttributeRouteModel = new AttributeRouteModel(
	//		//			new RouteAttribute($"api/{controller.ControllerName}")),
	//		//	});
	//		//}
	//	}
	//}

	//public class ModularControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature> {

	//	public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) {

	//		var currentAssembly = typeof(ModularControllerFeatureProvider).Assembly;
	//		var candidates = currentAssembly.GetExportedTypes()
	//			.Where(x => x.GetCustomAttributes<ApiComponentAttribute>().Any());

	//		foreach(var c in candidates) {
	//			feature.Controllers.Add(c.GetTypeInfo());
	//		}

	//		//feature.Controllers.Add(typeof(ChannelsController<bool>).GetTypeInfo());
	//		//feature.Controllers.Add(typeof(ChannelsController<int>).GetTypeInfo());
	//		//feature.Controllers.Add(typeof(ChannelsController<TempReading>).GetTypeInfo());
	//		//feature.Controllers.Add(typeof(ChannelsController<double>).GetTypeInfo());
	//	}
}
