using System.Threading.Tasks;

namespace Leviathan.Services.SDK {
	public interface IAsyncInitialize {
		Task Initialize { get; }
	}
}