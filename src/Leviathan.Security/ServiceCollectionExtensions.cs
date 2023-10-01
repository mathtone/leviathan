using Microsoft.Extensions.DependencyInjection;

namespace Leviathan.Security {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddKeys(this IServiceCollection services, Dictionary<string, Func<string, string>> keySelectors) =>
			services.AddSingleton<IKeyProvider>(new KeyProvider(keySelectors));
	}
}
