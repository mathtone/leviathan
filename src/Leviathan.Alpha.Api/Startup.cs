using Leviathan.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leviathan.System;
using Leviathan.Alpha.Npgsql;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Leviathan.Alpha.Configuration;
using Leviathan.DataAccess;
using Npgsql;
using Leviathan.DataAccess.Npgsql;
using Leviathan.RNG;
using Leviathan.Alpha.FactoryReset;
using Leviathan.Alpha.Components;
using Leviathan.Alpha.Services;
//using Leviathan.RNG;

namespace Leviathan.Alpha.Api {
	public class Startup {

		public IConfiguration Configuration { get; }
		private ILeviathan TheLeviathan { get; set; }

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers()
				.AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.Alpha.Api", Version = "v1" });
			});

			services.LeviathanWakes();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime) {

			this.TheLeviathan = app.ApplicationServices.GetService<ILeviathan>();
			lifetime.ApplicationStarted.Register(OnStart);
			lifetime.ApplicationStopping.Register(OnStop);

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();

			}
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leviathan.Alpha.Api v1"));
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}

		protected void OnStart() => TheLeviathan.Start();
		protected void OnStop() => TheLeviathan.Stop();
	}

	public static class LeviathanServices {
		public static IServiceCollection LeviathanWakes(this IServiceCollection services) => services
			.AddSingleton<ISystemConfigService<AlphaSystemConfiguration>, AlphaSystemConfigurationService>()
			.AddSingleton<ISystemConfigProvider<AlphaSystemConfiguration>>(svc => svc.GetRequiredService<ISystemConfigService<AlphaSystemConfiguration>>())
			.AddSingleton<IDbConnectionProvider<NpgsqlConnection, string>, NpgsqlConnectionProvider>()
			.AddSingleton<IStartupService, StartupService>()
			.AddSingleton<IDataSystemService<NpgsqlConnection>, DataSystemService>()
			.AddSingleton<IDataSystemService>(svc => svc.GetRequiredService<IDataSystemService<NpgsqlConnection>>())
			.AddSingleton<ILeviathan, TheLeviathan>()
			.AddSingleton<IRandom, CryptoRNG>()
			.AddSingleton<IFactoryResetService, FactoryResetService>()
			.AddSingleton<ILeviathanComponentsService, LeviathanComponentsService>()
			.AddSingleton<IQuickStartService, QuickStartService>();
	}
}