using System;

namespace Leviathan.Services {
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class HostedSingletonServiceAttribute : ServiceComponentAttribute {
		public HostedSingletonServiceAttribute(Type primaryServiceType, params Type[] secondaryServiceTypes) :
			base(primaryServiceType, secondaryServiceTypes) {
		}
	}
}
