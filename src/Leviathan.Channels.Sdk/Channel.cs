using System;
using System.Threading.Tasks;

namespace Leviathan.Channels.Sdk {

	public interface IChannel {
		int Id { get; }
		string Name { get; }
	}

	public interface IAsyncInputChannel : IChannel {
		Task<object> GetAsync();
	}

	public interface IAsyncInputChannel<T> : IAsyncInputChannel {
		new Task<T> GetAsync();
	}

	public interface IAsyncOutputChannel : IChannel {
		Task SetAsync(object value);
	}

	public interface IAsyncOutputChannel<T> : IAsyncOutputChannel {
		Task SetAsync(T value);
	}

	public interface IAsyncInputOutputChannel : IAsyncInputChannel, IAsyncOutputChannel { }
	public interface IAsyncInputOutputChannel<T> : IAsyncInputOutputChannel, IAsyncInputChannel<T>, IAsyncOutputChannel<T> { }

	public interface IInputChannel : IChannel {
		object Get();
	}

	public interface IInputChannel<T> : IInputChannel {
		new T Get();
	}

	public interface IOutputChannel : IChannel {
		void Set(object value);
	}
	public interface IOutputChannel<T> : IOutputChannel {
		void Set(T value);
	}

	public interface IInputOutputChannel : IInputChannel, IOutputChannel { }
	public interface IInputOutputChannel<T> : IInputOutputChannel, IInputChannel<T>, IOutputChannel<T> { }

	public abstract class ChannelBase : IChannel {
		public int Id { get; init; }
		public string Name { get; init; }
	}

	public abstract class AsyncInputChannelBase<T> : ChannelBase, IAsyncInputChannel<T> {
		public abstract Task<T> GetAsync();
		async Task<object> IAsyncInputChannel.GetAsync() => await GetAsync();
	}

	public abstract class AsyncOutputChannelBase<T> : ChannelBase, IAsyncOutputChannel<T> {
		public abstract Task SetAsync(T value);
		public abstract Task SetAsync(object value);
	}

	public abstract class AsyncIOChannelBase<T> : AsyncInputChannelBase<T>, IAsyncInputOutputChannel<T> {
		public abstract Task SetAsync(T value);
		public abstract Task SetAsync(object value);
	}

	public abstract class InputChannelBase<T> : ChannelBase, IInputChannel<T> {
		public abstract T Get();
		object IInputChannel.Get() => Get();
	}

	public abstract class OutputChannelBase<T> : ChannelBase, IOutputChannel<T> {
		public abstract void Set(T value);
		public abstract void Set(object value);
	}

	public abstract class IOChannelBase<T> : InputChannelBase<T>, IOutputChannel<T> {
		public abstract void Set(T value);
		public abstract void Set(object value);
	}
}