using Microsoft.Extensions.Hosting;

namespace Leviathan.SDK {
	public interface ILeviathanHostEnvironment {
		IHostEnvironment Environment { get; }
		IHostApplicationLifetime Lifetime { get; }
	}
}
