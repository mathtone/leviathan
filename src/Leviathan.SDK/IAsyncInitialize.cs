using System.Threading.Tasks;

namespace Leviathan.SDK {
	public interface IAsyncInitialize {
		Task Initialize { get; }
	}
}
