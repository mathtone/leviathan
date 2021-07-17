using Leviathan.Components.SDK;
using System;

namespace Leviathan.Services.SDK {
	public abstract class ServiceComponentAttribute : LeviathanComponentAttribute {

		public Type[] ServiceTypes { get; }

		public ServiceComponentAttribute(params Type[] serviceTypes) {
			this.ServiceTypes = serviceTypes;
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SingletonServiceAttribute : ServiceComponentAttribute {

		public SingletonServiceAttribute(params Type[] serviceType) :
			base(serviceType) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class TransientServiceAttribute : ServiceComponentAttribute {
		public TransientServiceAttribute(params Type[] serviceTypes) :
			base(serviceTypes) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ScopedServiceAttribute : ServiceComponentAttribute {
		public ScopedServiceAttribute(params Type[] serviceTypes) :
			base(serviceTypes) {
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class HostedServiceAttribute : ServiceComponentAttribute {
		public HostedServiceAttribute(params Type[] serviceTypes) :
			base(serviceTypes) {
		}
	}
}