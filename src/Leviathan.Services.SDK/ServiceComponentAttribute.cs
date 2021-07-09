﻿using System;

namespace Leviathan.Services.SDK {
	public abstract class ServiceAttribute : LeviathanComponentAttribute {

		public Type ServiceType { get; }

		public ServiceAttribute(Type serviceType) {
			this.ServiceType = serviceType;
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SingletonServiceAttribute : ServiceAttribute {

		public SingletonServiceAttribute(Type serviceType) :
			base(serviceType) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class TransientServiceAttribute : ServiceAttribute {
		public TransientServiceAttribute(Type serviceType) :
			base(serviceType) {
		}
	}

	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ScopedServiceAttribute : ServiceAttribute {
		public ScopedServiceAttribute(Type serviceType) :
			base(serviceType) {
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class HostedServiceAttribute : ServiceAttribute {
		public HostedServiceAttribute(Type serviceType) :
			base(serviceType) {
		}
	}

	public sealed class ApiComponentAttribute : Attribute {
		public ApiComponentAttribute() {
			;
		}
	}
}