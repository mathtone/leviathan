using System;
using System.Threading.Tasks;

namespace Leviathan.Channels.SDK {

	public interface IOutputChannel<in T> {
		void SetValue(T value);

	}

	public interface IAsyncOutputChannel<in T> {
		Task SetValue(T value);
	}
}
