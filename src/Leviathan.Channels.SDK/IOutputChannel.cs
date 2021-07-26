using System;
using System.Threading.Tasks;

namespace Leviathan.Channels.SDK {

	public interface IOutputChannel : IChannel {
		void SetValue(object value);
	}

	public interface IAsyncOutputChannel : IChannel {
		Task SetValueAsync(object value);
	}

	public interface IOutputChannel<in T> : IOutputChannel {
		void SetValue(T value);
	}

	public interface IAsyncOutputChannel<T> : IAsyncOutputChannel {
		Task SetValueAsync(T value);
	}
}