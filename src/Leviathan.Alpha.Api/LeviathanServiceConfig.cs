using Leviathan.Alpha.Core;
using Leviathan.Alpha.System;
using Leviathan.Alpha.TestModule;
using Leviathan.Services.SDK;
using Leviathan.System.SDK;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Leviathan.Alpha.Api {
	public static class LeviathanServiceConfig {

		public static IServiceCollection AddTheLeviathan(this IServiceCollection services) => services
			.AddSingleton<ILeviathanSystem, SystemService>()
			.AddSingleton<ITheLeviathan, TheLeviathan>()
			.ScanForServiceComponents();

		static IServiceCollection ScanForServiceComponents(this IServiceCollection services) {


			var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			;
			var moduleAssemblies = new Dictionary<string, Assembly>();

			foreach (var a in AppDomain.CurrentDomain.GetAssemblies()) {
				if (Path.GetDirectoryName(a.Location) == dir) {
					moduleAssemblies.Add(a.Location, a);
				}
			}

			foreach (var file in Directory.GetFiles(dir, "*.dll")) {
				if (!moduleAssemblies.ContainsKey(file)) {
					moduleAssemblies.Add(file, Assembly.LoadFile(file));
				}
			}

			foreach (var a in moduleAssemblies.Values) {
				foreach (var iType in a.GetExportedTypes()) {
					var attr = iType.GetCustomAttribute<ServiceComponentAttribute>();
					if (attr != null) {
						var sType = attr.RegistrationType;
					
						services.AddSingleton(sType, iType);
						;
					}
				}
			}
			services.AddSingleton(typeof(ITestService), typeof(TestService));
			return services;
		}

		public static IApplicationBuilder AwakenTheLeviathan(this IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime) {

			var svc = app.ApplicationServices.GetRequiredService<ITestService>();
			var leviathan = app.ApplicationServices.GetService<ITheLeviathan>();

			lifetime.ApplicationStarted.Register(async () => await leviathan.Start());
			lifetime.ApplicationStopping.Register(async () => await leviathan.Stop());
			return app;
		}
	}
}