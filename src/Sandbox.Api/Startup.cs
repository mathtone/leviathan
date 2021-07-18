using Leviathan.Services.SDK;
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sandbox.Api {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {

			var path = AppDomain.CurrentDomain.BaseDirectory;
			//var loaded = AppDomain.CurrentDomain.GetAssemblies().ToDictionary(a => a.FullName);
			AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;

			var files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
			var apiControllerAssemblies = new List<Assembly>();
			foreach (var f in files) {
				var name = AssemblyName.GetAssemblyName(f);
				var assembly = Assembly.Load(name);
				var containsApiControllers = false;
				foreach (var t in assembly.GetExportedTypes()) {

					if (t.IsAssignableTo(typeof(ControllerBase))) {
						containsApiControllers = true;
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
					}
				}
				if (containsApiControllers) {
					apiControllerAssemblies.Add(assembly);
				}
			}

			//var loaded = AppDomain.CurrentDomain.GetAssemblies().ToDictionary(a => a.FullName);
			services.AddControllers();
			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sandbox.Api", Version = "v1" });
			});
		}

		private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args) {
			;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sandbox.Api v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
