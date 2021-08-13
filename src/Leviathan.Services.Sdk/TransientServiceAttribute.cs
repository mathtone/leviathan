using System;

namespace Leviathan.Services {
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class TransientServiceAttribute : ServiceComponentAttribute {
		public TransientServiceAttribute(Type primaryServiceType, params Type[] secondaryServiceTypes) :
			base(primaryServiceType, secondaryServiceTypes) {
		}
	}
}
