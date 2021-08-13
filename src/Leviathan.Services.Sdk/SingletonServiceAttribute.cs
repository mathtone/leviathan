using System;

namespace Leviathan.Services {
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SingletonServiceAttribute : ServiceComponentAttribute {

		public SingletonServiceAttribute(Type primaryServiceType, params Type[] secondaryServiceTypes) :
			base(primaryServiceType, secondaryServiceTypes) {
		}
	}
}