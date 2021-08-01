using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

			services.AddHostedSingleton(typeof(ITestService), typeof(TestService));
			services.AddControllers();
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
}