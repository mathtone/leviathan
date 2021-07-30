using System;

namespace Leviathan.Services.Sdk {
	public abstract class ServiceComponentAttribute : Attribute {

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
}