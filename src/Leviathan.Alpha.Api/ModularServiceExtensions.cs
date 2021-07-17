using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.IO;
using Leviathan.Utilities;
using Leviathan.Services.SDK;

namespace Leviathan.Alpha.Api {
	public static class ModularServiceExtensions {
		public static IServiceCollection AddModularServices(this IServiceCollection services) {

			var localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var dlls = Directory.GetFiles(localPath, "*.dll");
			var loaded = AssemblyLoader.GetLoadedAssemblies();
			var assemblies = AppDomain.CurrentDomain
				.GetAssemblies()
				.Where(a => Path.GetDirectoryName(a.Location) == localPath)
				.ToDictionary(a => a.Location);

			foreach (var a in assemblies.Values) {
				foreach (var t in a.GetExportedTypes()) {
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
			}

			return services;
		}
	}
}