using System;

namespace Leviathan.Services {
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ScopedServiceAttribute : ServiceComponentAttribute {
		public ScopedServiceAttribute(Type primaryServiceType, params Type[] secondaryServiceTypes) :
			base(primaryServiceType, secondaryServiceTypes) {
		}
	}
}
