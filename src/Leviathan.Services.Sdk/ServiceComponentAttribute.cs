using Leviathan.Components.Sdk;
using System;

namespace Leviathan.Services {
	public abstract class ServiceComponentAttribute : LeviathanComponentAttribute {

		public Type PrimaryServiceType { get; }
		public Type[] SecondaryServiceTypes { get; }

		public ServiceComponentAttribute(Type primaryServiceType, params Type[] secondaryServiceTypes) {
			this.PrimaryServiceType = primaryServiceType;
			this.SecondaryServiceTypes = secondaryServiceTypes;
		}
	}
}