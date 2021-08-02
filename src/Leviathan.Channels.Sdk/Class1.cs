using System;
using System.Threading.Tasks;

namespace Leviathan.Channels.Sdk {
	public interface IChannel {}

	public interface IInputChannel : IChannel {
		object Get();
	}

	public interface IInputChannel<out T> : IInputChannel {
		T Get();
	}

	public interface IAsyncInputChannel : IChannel {
		Task<string> GetAsync();
	}

	public interface IAsyncInputChannel<T> : IAsyncInputChannel {
		Task<T> GetAsync();
	}


	public interface IOutputChannel : IChannel {
		void Set(object value);
	}

	public interface IOutputChannel<in T> : IOutputChannel {
		void Set(T value);
	}

	public interface IAsyncOutputChannel : IChannel {
		Task SetAsync(object value);
	}

	public interface IAsyncOutputChannel<T> : IAsyncOutputChannel {
		Task SetAsync(T value);
	}

	public interface IAsyncInOutChannel<T> : IAsyncInputChannel<T>, IAsyncOutputChannel<T> { }
	public interface IInOutChannel<T> : IInputChannel<T>, IOutputChannel<T> { }
}
