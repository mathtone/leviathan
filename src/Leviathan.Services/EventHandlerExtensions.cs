using System;

namespace Leviathan.Services {
	public static class EventHandlerExtensions {
		public static void Raise<S, T>(this EventHandler handler, S sender, T args) where T : EventArgs => handler?.Invoke(sender, args);
		public static void TryInvoke<S, T>(this Action<S, T> handler, S sender, T args) => handler?.Invoke(sender, args);
	}
}