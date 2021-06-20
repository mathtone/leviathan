using Leviathan.SDK;
using Microsoft.Extensions.Hosting;

namespace Leviathan.Alpha.Core {
	
	public class LeviathanHostEnvironment : ILeviathanHostEnvironment {

		public IHostEnvironment Environment { get; init; }
		public IHostApplicationLifetime Lifetime { get; init; }

		public LeviathanHostEnvironment(IHostEnvironment environment, IHostApplicationLifetime lifetime) {
			this.Environment = environment;
			this.Lifetime = lifetime;
		}
	}
}