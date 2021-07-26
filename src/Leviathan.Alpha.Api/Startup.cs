using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using System.IO;
using Leviathan.Utilities;
using System;
using System.Linq;
using Leviathan.Services.SDK;
using Microsoft.AspNetCore.Mvc;
using Leviathan.Alpha.SystemConfiguration;

namespace Leviathan.Alpha.Api {
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
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.API", Version = "v1" });
			});
		}

		private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args) {
			;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

			//var cfg = app.ApplicationServices.GetRequiredService<ISystemConfigurationService>();
			//cfg.ApplyProfile("RoboTankProfile", new ProfileApplication[] {
			//	new(){
			//		ProfileName = "BasicProfile",
			//		ApplicationFields = new ApplicationField[] {
			//			new() {
			//				Name = "Instance Name",
			//				Value = "Leviathan"
			//			}
			//		}
			//	},
			//	new(){
			//		ProfileName = "PostgreSQLProfile",
			//		ApplicationFields = new ApplicationField[] {
			//			new() {
			//				Name = "Host Name",
			//				Value = "leviathan-alpha"
			//			},
			//			new() {
			//				Name = "Instance DB Name",
			//				Value = "leviathan-alpha-db"
			//			},
			//			new() {
			//				Name = "DB Login",
			//				Value = "pi"
			//			},
			//			new() {
			//				Name = "DB Password",
			//				Value = "Digital!2021"
			//			}
			//		}
			//	},
			//	new(){
			//		ProfileName = "RoboTankProfile",
			//		ApplicationFields = Array.Empty<ApplicationField>()
			//	}
			//});

			/*
			{
				"profileName": "BasicProfile",
				"applicationFields": [
				  {
					"name": "Instance Name",
					"description": "The name of this instance of THE LEVIATHAN.",
					"value": "TheLeviathan"
				  }
				]
			  },
			  {
				"profileName": "PostgreSQLProfile",
				"applicationFields": [
				  {
					"name": "Host Name",
					"description": "Database server host name.",
					"value": null
				  },
				  {
					"name": "Instance DB Name",
					"description": "Database name.",
					"value": null
				  },
				  {
					"name": "DB Login",
					"description": "Database login.",
					"value": null
				  },
				  {
					"name": "DB Password",
					"description": "Database password.",
					"value": null
				  }
				]
			  },
			  {
				"profileName": "RoboTankProfile",
				"applicationFields": []
			  }
			*/

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				
			}
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sandbox.Api v1"));
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
