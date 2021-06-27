using Leviathan.Alpha.Components;
using Leviathan.Alpha.Configuration;
using Leviathan.Alpha.Core;
using Leviathan.Alpha.Data.Npgsql;
using Leviathan.Alpha.Database;
using Leviathan.Alpha.Hardware;
using Leviathan.Alpha.Startup;
using Leviathan.Components;
using Leviathan.SDK;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api {
	public abstract class ConfigFileManager<T> : ConfigServiceBase<T> where T : new() {
		public override T Config { get; protected set; }

		protected static string LocalPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		public ConfigFileManager(IConfiguration configuration, string sectionName) {
			configuration.GetSection(sectionName).Bind(Config = new T());
		}
	}

	public class LeviathanConfigManager<T> : ConfigFileManager<T> where T : new() {
		public LeviathanConfigManager(IConfiguration configuration) : base(configuration, typeof(T).Name) { }
	}

	public static class ServiceConfiguration {
		public static IServiceCollection AddTheLeviathan(this IServiceCollection services) => services
			.AddSingleton<IAmLeviathan, TheLeviathan>()
			.AddSingleton<ILeviathanSystem, LeviathanSystem>()
			.AddSingleton<IStartupService, StartupService>()
			.AddSingleton<IComponentsService, ComponentsService>()
			.AddSingleton<IDataSystemService<NpgsqlConnection>, NpgsqlDataSystemService>()
			.AddSingleton<IDataSystemService<IDbConnection>>(svc => svc.GetRequiredService<IDataSystemService<NpgsqlConnection>>())
			.AddSingleton<IDataSystemService>(svc => svc.GetRequiredService<IDataSystemService<IDbConnection>>())
			.AddSingleton<IConfigManager<DatabaseConfig>, LeviathanConfigManager<DatabaseConfig>>()
			.AddSingleton<ILeviathanAlphaDataContextProvider, LeviathanAlphaDataContextProvider>()
			.AddSingleton<ILeviathanHardwareSystemService, LeviathanHardwareSystemService>()
			.AddTransient(svc => svc.GetRequiredService<IDataSystemService<NpgsqlConnection>>().InstanceDB);

		//.AddSingleton<IListRepository<long, ComponentCategoryRecord>>(svc => new ComponentCategoryRepo(null, null));


		public static IApplicationBuilder AwakenTheLeviathan(this IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime) {
			var leviathan = app.ApplicationServices.GetService<IAmLeviathan>();
			lifetime.ApplicationStarted.Register(async () => await leviathan.Start(new LeviathanHostEnvironment(env, lifetime)));
			lifetime.ApplicationStopping.Register(async () => await leviathan.Stop());
			return app;
		}
	}
}