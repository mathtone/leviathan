using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Leviathan.Services {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddHostingTo<SVC>(this IServiceCollection services) where SVC : notnull =>
			services.AddTransient(svc => (IHostedService)svc.GetRequiredService<SVC>());


		public static IServiceCollection ConfigureSection<T>(this IServiceCollection services, string? sectionName = default) where T : class, new() {
			return services.AddSingleton(svc => {
				var cfg = svc.GetRequiredService<IConfiguration>().GetSection(sectionName ?? typeof(T).Name);
				var rtn = new T();
				cfg.Bind(rtn);
				return rtn;
			});
		}
	}
}
