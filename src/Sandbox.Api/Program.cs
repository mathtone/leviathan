using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Api {
	public class Program {
		public static void Main(string[] args) {
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => {
					webBuilder.UseStartup<LeviathanHostStartup>();
				});
	}

	public class LeviathanHostStartup {

		public IConfiguration Configuration { get; }

		public LeviathanHostStartup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services) {

			services.AddHostedSingleton(typeof(ITestService), typeof(TestService))
				.AddControllers(o => o.Conventions.Add(new GenericControllerRouteConvention()))
				.ConfigureApplicationPartManager(mgr =>
					mgr.FeatureProviders.Add(new GenericTypeControllerFeatureProvider(services))
				);
			services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sandbox.Api", Version = "v1" }));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger()
				.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sandbox.Api v1"))
				.UseHttpsRedirection()
				.UseRouting()

				.UseAuthorization()
				.UseEndpoints(endpoints => {
					endpoints.MapControllers();
				});
		}
	}

	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddHostedSingleton(this IServiceCollection services, Type serviceType, Type implementationType) {
			var meth = typeof(ServiceCollectionExtensions)
				.GetMethod(nameof(AddHostedSingleton), 2, new[] { typeof(IServiceCollection) })
				.MakeGenericMethod(serviceType, implementationType);
			meth.Invoke(null, new[] { services });
			return services;
		}

		public static IServiceCollection AddHostedSingleton<TService, TImplementation>(this IServiceCollection services)
			where TService : class, IHostedService
			where TImplementation : class, TService {
			services.AddSingleton<TService, TImplementation>();
			services.AddHostedService(svc => svc.GetRequiredService<TService>());
			return services;
		}
	}

	public interface ITestService : IHostedService {
	}

	public class TestService : ITestService {
		public TestService() {
			;
		}
		public Task StartAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}
	}

	public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature> {
		
		IServiceCollection _services;

		public GenericTypeControllerFeatureProvider(IServiceCollection services) {
			_services = services;
		}

		public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) {
			
			;
			//var currentAssembly = typeof(GenericTypeControllerFeatureProvider).Assembly;
			//var candidates = currentAssembly.GetExportedTypes().Where(x => x.GetCustomAttributes<GeneratedControllerAttribute>().Any());

			//foreach (var candidate in candidates) {
			//	feature.Controllers.Add(
			//		typeof(BaseController<>).MakeGenericType(candidate).GetTypeInfo()
			//	);
			//}
		}
	}

	public class GenericControllerRouteConvention : IControllerModelConvention {
		public void Apply(ControllerModel controller) {
			if (controller.ControllerType.IsGenericType) {
				var genericType = controller.ControllerType.GenericTypeArguments[0];
				//var customNameAttribute = genericType.GetCustomAttribute<GeneratedControllerAttribute>();

				//if (customNameAttribute?.Route != null) {
				//	controller.Selectors.Add(new SelectorModel {
				//		AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(customNameAttribute.Route)),
				//	});
				//}
			}
		}
	}
}