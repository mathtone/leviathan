using System;

namespace Leviathan.Utilities {
	public static class IDisposableExtensions {
		public static void TryDispose<T>(this T target, Action<T> action) where T : IDisposable {
			try {
				action(target);
			}
			finally {
				target.Dispose();
			}
		}

		public static R TryDispose<T,R>(this T target, Func<T,R> func) where T : IDisposable {
			try {
				return func(target);
			}
			finally {
				target.Dispose();
			}
		}
	}
}