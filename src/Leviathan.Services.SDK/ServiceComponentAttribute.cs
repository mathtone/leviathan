using System;

namespace Leviathan.Services.SDK {
	[AttributeUsage(AttributeTargets.Class)]
	public class ServiceComponentAttribute : LeviathanComponentAttribute {

		public Type RegistrationType { get; }

		public ServiceComponentAttribute(Type serviceRegistrationType) {
			this.RegistrationType = serviceRegistrationType;
		}
	}
}