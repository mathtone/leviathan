using System.Threading.Tasks;

namespace Leviathan.Channels.SDK {
	public interface IInputChannel<out T> : IChannel {
		T GetValue();
	}

	public interface IAsyncInputChannel<T> : IChannel {
		Task<T> GetValue();
	}
}
