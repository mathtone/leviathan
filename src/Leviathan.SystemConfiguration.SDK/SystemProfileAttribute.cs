using Leviathan.Components.SDK;
using System;

namespace Leviathan.SystemConfiguration.SDK {
	public class SystemProfileAttribute : LeviathanComponentAttribute {

	}

	public interface ISystemProfile {
		void Apply();
	}

	public abstract class SystemProfileComponent : ISystemProfile {
		public abstract void Apply();
	}
}