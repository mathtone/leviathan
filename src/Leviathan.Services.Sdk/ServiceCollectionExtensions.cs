using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Services {

	public static class ServiceCollectionExtensions {

		public static IServiceCollection AddHostedSingleton(this IServiceCollection services, Type serviceType, Type implementationType) {
			var meth = typeof(ServiceCollectionExtensions)
				.GetMethod(nameof(AddHostedSingleton), new[] { typeof(IServiceCollection) })
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
}