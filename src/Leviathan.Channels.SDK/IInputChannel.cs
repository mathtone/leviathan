using System.Threading.Tasks;

namespace Leviathan.Channels.SDK {

	public interface IInputChannel : IChannel {
		object GetValue();
	}
	
	public interface IAsyncInputChannel : IChannel {
		Task<object> GetValueAsync();
	}

	public interface IInputChannel<out T> : IInputChannel {
		new T GetValue();
	}

	public interface IAsyncInputChannel<T> : IAsyncInputChannel {
		new Task<T> GetValueAsync();
	}
}