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

namespace Leviathan.Alpha.Api {
	public class Startup {
		public IConfiguration Configuration { get; }
		Dictionary<string, Assembly> _loadedAssemblies;
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services) {

			var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var sdkPath = Path.Combine(basePath, "Sdk");
			var modulePath = Path.Combine(basePath, "Modules");

			_loadedAssemblies = AppDomain.CurrentDomain
				.GetAssemblies()
				.ToDictionary(a => a.FullName);

			AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
			foreach (var f in Directory.EnumerateFiles(sdkPath, "*.dll", SearchOption.AllDirectories)) {
				var name = AssemblyName.GetAssemblyName(f);

				if (!_loadedAssemblies.ContainsKey(name.FullName)) {
					Assembly.LoadFile(f);
				}
			}

			foreach (var f in Directory.EnumerateFiles(modulePath, "*.dll", SearchOption.AllDirectories)) {
				var name = AssemblyName.GetAssemblyName(f);

				if (!_loadedAssemblies.ContainsKey(name.FullName)) {
					Assembly.LoadFile(f);
				}
			}

			//foreach (var f in Directory.EnumerateFiles(modulePath, "*.dll", SearchOption.AllDirectories)) {
			//	var name = AssemblyName.GetAssemblyName(f);

			//	if (!_loadedAssemblies.ContainsKey(name.FullName)) {
			//		Assembly.LoadFile(f);
			//	}
			//}

			var moduleAssemblies = AppDomain.CurrentDomain
				.GetAssemblies()
				.Where(a => Path.GetDirectoryName(a.Location).StartsWith(modulePath))
				.ToDictionary(a => a.Location);

			var apiControllerAssemblies = new List<Assembly>();
			foreach (var a in moduleAssemblies.Values) {
				var containsApiControllers = false;
				foreach (var t in a.GetExportedTypes()) {

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
					apiControllerAssemblies.Add(a);
				}
			}

			var mvcBuild = services.AddControllers(o => o.Conventions.Add(new ModularControllerRouteConvention()))
				.AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

			foreach(var a in apiControllerAssemblies) {
				mvcBuild.AddApplicationPart(a);
			}
			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.Alpha.Api", Version = "v1" });
			});
		}

		private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) =>
			_loadedAssemblies[args.Name];

		private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args) =>
			_loadedAssemblies.Add(args.LoadedAssembly.FullName, args.LoadedAssembly);

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leviathan.Alpha.Api v1"));
			}

			app.AwakenTheLeviathan(env, lifetime)
				.UseHttpsRedirection()
				.UseRouting()
				.UseAuthorization()
				.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
