using Leviathan.Components.SDK;
using System;

namespace Leviathan.Services.SDK {
	public abstract class ServiceAttribute : LeviathanComponentAttribute {

		public Type[] ServiceTypes { get; }

		public ServiceAttribute(params Type[] serviceTypes) {
			this.ServiceTypes = serviceTypes;
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SingletonServiceAttribute : ServiceAttribute {

		public SingletonServiceAttribute(params Type[] serviceType) :
			base(serviceType) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class TransientServiceAttribute : ServiceAttribute {
		public TransientServiceAttribute(params Type[] serviceTypes) :
			base(serviceTypes) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ScopedServiceAttribute : ServiceAttribute {
		public ScopedServiceAttribute(params Type[] serviceTypes) :
			base(serviceTypes) {
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class HostedServiceAttribute : ServiceAttribute {
		public HostedServiceAttribute(params Type[] serviceTypes) :
			base(serviceTypes) {
		}
	}
}