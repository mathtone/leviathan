namespace Leviathan.SystemHostLauncher {
	public static class ServiceCollectionExtensions {
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