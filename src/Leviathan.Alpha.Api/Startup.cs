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
using System.IO;
using Leviathan.Utilities;
using Leviathan.Services.SDK;

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
				.AddModularServices()
				.AddControllers(o => o.Conventions.Add(new ModularControllerRouteConvention()))
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

	public class ModularControllerRouteConvention : IControllerModelConvention {
		public void Apply(ControllerModel controller) {
			var attr = controller.ControllerType.GetCustomAttribute<ApiComponentAttribute>();
			if (attr != null) {
				controller.Selectors.Add(new SelectorModel {
					AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"api/{controller.ControllerName}")),
				});
			}
		}
	}


	public static class ModularServiceExtensions {

		public static IServiceCollection AddModularServices(this IServiceCollection services) {
			var localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var dlls = Directory.GetFiles(localPath, "*.dll");
			var loaded = AssemblyLoader.GetLoadedAssemblies();
			var assemblies = AppDomain.CurrentDomain.GetAssemblies()
				.Where(a => Path.GetDirectoryName(a.Location) == localPath)
				.ToDictionary(a => a.Location);

			foreach (var a in assemblies.Values) {
				foreach (var t in a.GetExportedTypes()) {
					var attr = t.GetCustomAttribute<ServiceAttribute>();
					if (attr != null) {
						if (attr is SingletonServiceAttribute) services.AddSingleton(attr.ServiceType, t);
						if (attr is TransientServiceAttribute) services.AddTransient(attr.ServiceType, t);
						if (attr is ScopedServiceAttribute) services.AddScoped(attr.ServiceType, t);
					}
				}
			}

			return services;
		}
	}
}
