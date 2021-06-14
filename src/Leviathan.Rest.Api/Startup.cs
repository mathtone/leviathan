using Leviathan.Core;
using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Leviathan.Hardware;
using Leviathan.Hardware.Npgsql;
using Leviathan.Initialization;
using Leviathan.Plugins;
using Leviathan.Plugins.Npgsql;
using Leviathan.SDK;
using Leviathan.System;
using Leviathan.System.Npgsql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Rest.Api {
	public class Startup {

		ILeviathanCore Core;

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {

			var dbProvider = new NpgsqlConnectionProvider(null);
			var cfg = new SystemConfiguration {
				DbLogin = "pi",
				InstanceName = "Leviathan0x00",
				HostName = "poseidonalpha.local",
				DbPassword = "Digital!2021"
			};
			//var server = new NpgsqlConnectionProvider($"Host={cfg.HostName};Username={cfg.DbLogin};Database=postgres;Password={cfg.DbPassword};");
			//var db = new NpgsqlConnectionProvider(dbProvider);
			//Core = new LeviathanCore(cfg,new SystemDBData(
			
			//)

			services.AddSingleton<ISystemConfiguration>(cfg);
			services.AddSingleton<IDbConnectionProvider>(dbProvider);
			services.AddSingleton<IDbConnectionProvider<NpgsqlConnection>>(dbProvider);
			services.AddSingleton<ISystemDbData, SystemDBData>();
			
			services.AddSingleton<IComponentData, ComponentData>();
			services.AddSingleton<IComponentCategoryData, ComponentCategoryData>();
			services.AddSingleton<IComponentService, ComponentService>();
			//services.AddSingleton<IInitializationService, InitializationService>();
			//services.AddSingleton<ILeviathanCore, InitializationService>();
			services.AddSingleton<ILeviathanCore, LeviathanCore>();


			//services.AddSingleton<ISystemDbService, SystemService>();
			//services.AddSingleton<IChannelTypeData, ChannelTypeData>();
			//services.AddSingleton<IHardwareService, HardwareService>();
			//services.AddSingleton<IHardwareModuleTypeData, HardwareModuleTypeData>();

			services.AddControllers();
			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.Rest.Api", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime) {
			this.Core = app.ApplicationServices.GetRequiredService<ILeviathanCore>();
			lifetime.ApplicationStarted.Register(OnStart);
			lifetime.ApplicationStopping.Register(OnStop);

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leviathan.Rest.Api v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}

		protected void OnStart() => Core.Start();
		protected void OnStop() => Core.Stop();
	}
}
