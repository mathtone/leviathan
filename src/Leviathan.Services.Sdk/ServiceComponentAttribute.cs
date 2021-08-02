using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Leviathan.Services.Sdk {

	//[AttributeUsage(AttributeTargets.Method, AllowMultiple =false)]
	//public class ServiceConfigurationAttribute : Attribute {

	//}

	public abstract class ServiceComponentAttribute : Attribute {

		public Type PrimaryServiceType { get; }
		public Type[] SecondaryServiceTypes { get; }

		public ServiceComponentAttribute(Type primaryServiceType,params Type[] secondaryServiceTypes) {
			this.PrimaryServiceType = primaryServiceType;
			this.SecondaryServiceTypes = secondaryServiceTypes;
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SingletonServiceAttribute : ServiceComponentAttribute {

		public SingletonServiceAttribute(Type primaryServiceType,params Type[] secondaryServiceTypes) :
			base(primaryServiceType,secondaryServiceTypes) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class TransientServiceAttribute : ServiceComponentAttribute {
		public TransientServiceAttribute(Type primaryServiceType,params Type[] secondaryServiceTypes) :
			base(primaryServiceType, secondaryServiceTypes) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ScopedServiceAttribute : ServiceComponentAttribute {
		public ScopedServiceAttribute(Type primaryServiceType,params Type[] secondaryServiceTypes) :
			base(primaryServiceType, secondaryServiceTypes) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class HostedSingletonServiceAttribute : ServiceComponentAttribute {
		public HostedSingletonServiceAttribute(Type primaryServiceType,params Type[] secondaryServiceTypes) :
			base(primaryServiceType, secondaryServiceTypes) {
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

		//[ServiceConfiguration
	}

}