using Leviathan.Components;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Leviathan.Services {

	public abstract class ServiceComponentAttribute : LeviathanComponentAttribute {

		public ServiceLifetime Lifetime { get; }
		public Type PrimaryServiceType { get; }
		public Type[] SecondaryServiceTypes { get; }

		public ServiceComponentAttribute(ServiceLifetime lifetime, string description, Type primaryServiceType, params Type[] secondaryServiceTypes) : base(description) {
			this.PrimaryServiceType = primaryServiceType;
			this.SecondaryServiceTypes = secondaryServiceTypes;
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ScopedServiceAttribute : ServiceComponentAttribute {
		public ScopedServiceAttribute(Type primaryServiceType, params Type[] secondaryServiceTypes) :
			base(ServiceLifetime.Scoped, "Scoped Service", primaryServiceType, secondaryServiceTypes) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SingletonServiceAttribute : ServiceComponentAttribute {

		public SingletonServiceAttribute(Type primaryServiceType, params Type[] secondaryServiceTypes) :
			base(ServiceLifetime.Singleton, "Singleton Service", primaryServiceType, secondaryServiceTypes) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class TransientServiceAttribute : ServiceComponentAttribute {
		public TransientServiceAttribute(Type primaryServiceType, params Type[] secondaryServiceTypes) :
			base(ServiceLifetime.Transient, "Transient Service", primaryServiceType, secondaryServiceTypes) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class HostedAttribute : LeviathanComponentAttribute {
		public Type PrimaryServiceType { get; }
		public HostedAttribute() : base("Hosted Service") { }
		public HostedAttribute(Type primaryServiceType) : this() {
			PrimaryServiceType = primaryServiceType;
		}
	}
}