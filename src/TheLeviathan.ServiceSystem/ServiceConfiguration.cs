using Leviathan.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using TheLeviathan.ComponentSystem;

namespace TheLeviathan.ServiceSystem {

	public interface IServiceSystem {

	}

	[SingletonService(typeof(IServiceSystem))]
	public class ServiceSystem : IServiceSystem {
		
		IServiceProvider _services;


		public ServiceSystem(IServiceProvider services) {
			_services = services;
		}

		 
	}

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

					foreach (var attr in type.GetCustomAttributes()) {

						if (attr is ServiceComponentAttribute sc) {
							var primary = sc.PrimaryServiceType;

							if (attr is SingletonServiceAttribute)
								RegisterService(primary, sc.SecondaryServiceTypes, type, services.AddSingleton, s => services.AddSingleton(s, svc => svc.GetRequiredService(primary)));

							if (attr is TransientServiceAttribute)
								RegisterService(primary, sc.SecondaryServiceTypes, type, services.AddTransient, services.AddTransient);

							if (attr is ScopedServiceAttribute)
								RegisterService(primary, sc.SecondaryServiceTypes, type, services.AddScoped, services.AddScoped);
						}
						if (attr is HostedAttribute ha) {
							if (ha.PrimaryServiceType == null) {
								services.AddHosting(type);
							}
							else {
								services.AddHosting(ha.PrimaryServiceType, type);
							}
						}
					}
				}
			}

			return services;
		}

		public static IServiceCollection AddHosting(this IServiceCollection services, Type hostedServiceType) {
			typeof(ServiceCollectionHostedServiceExtensions)
				.GetMethod(nameof(ServiceCollectionHostedServiceExtensions.AddHostedService), 1, new[] { typeof(IServiceCollection) })
				.MakeGenericMethod(new[] { hostedServiceType })
				.Invoke(null, new[] { services });
			return services;
		}

		public static IServiceCollection AddHosting(this IServiceCollection services, Type hostedServiceType, Type implementationType) {
			typeof(ServiceConfiguration)
				.GetMethod(nameof(AddHosting), new[] { typeof(IServiceCollection) })
				.MakeGenericMethod(new[] { hostedServiceType })
				.Invoke(null, new[] { services });

			return services;
		}

		public static IServiceCollection AddHosting<TService>(this IServiceCollection services) where TService : class, IHostedService {

			services.AddHostedService(svc => svc.GetRequiredService<TService>());
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