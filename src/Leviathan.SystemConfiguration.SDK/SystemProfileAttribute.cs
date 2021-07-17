using Leviathan.Components.SDK;
using System;
using System.Threading.Tasks;

namespace Leviathan.SystemConfiguration.SDK {
	public class SystemProfileAttribute : LeviathanComponentAttribute {

	}

	public interface ISystemProfile {
		Task Apply();
	}

	public abstract class SystemProfileComponent : ISystemProfile {
		public abstract Task Apply();
	}
}