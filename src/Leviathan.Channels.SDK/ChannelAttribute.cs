
using Leviathan.Components.SDK;
using System;

namespace Leviathan.Channels.SDK {
	[AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
	public class ChannelAttribute : LeviathanComponentAttribute {
		public ChannelAttribute() {
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ChannelProviderAttribute : LeviathanComponentAttribute {

	}
}