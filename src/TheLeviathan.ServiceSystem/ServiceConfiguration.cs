using Leviathan.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace TheLeviathan.ServiceSystem {
	public static class ServiceConfiguration {
		public static IServiceCollection AddComponentServices(this IServiceCollection services) {

			var path = AppDomain.CurrentDomain.BaseDirectory;
			var files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);

			foreach (var f in files) {

				var name = AssemblyName.GetAssemblyName(f);
				var assembly = default(Assembly);
				try {
					assembly = Assembly.Load(name);
				}
				catch (Exception ex) {
					assembly = Assembly.LoadFile(f);
				}

				foreach (var type in assembly.GetExportedTypes()) {

					var attr = type.GetCustomAttribute<ServiceComponentAttribute>();

					if (attr != null) {

						var primary = attr.PrimaryServiceType;
						if (attr is SingletonServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, type, services.AddSingleton, s => services.AddSingleton(s, svc => svc.GetRequiredService(primary)));

						if (attr is TransientServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, type, services.AddTransient, services.AddTransient);

						if (attr is ScopedServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, type, services.AddScoped, services.AddScoped);

						if (attr is HostedSingletonServiceAttribute)
							RegisterService(primary, attr.SecondaryServiceTypes, type, services.AddHostedSingleton, services.AddSingleton);
					}
				}
			}

			//services.ConfigureMvc();
			return services;
		}


		static void RegisterService(Type primary, Type[] secondary, Type implementation, Func<Type, Type, IServiceCollection> registerAction, Func<Type, object> secondaryRegisterAction) {
			registerAction(primary, implementation);
			if (secondary != null) {
				foreach (var i in secondary) {
					secondaryRegisterAction(i);
				}
			}
		}
	}
}