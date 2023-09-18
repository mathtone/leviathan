using Microsoft.Extensions.DependencyInjection;

namespace Leviathan.Crypto {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddKeys(this IServiceCollection services,Dictionary<string,Func<string,string>> keySelectors)=>
			services.AddSingleton<IKeyProvider>(new KeyProvider(keySelectors));
	}
}