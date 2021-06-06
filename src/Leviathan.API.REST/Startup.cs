using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Leviathan.Hardware;
using Leviathan.QuickStart.RoboTank;
using Leviathan.Services.Core;
using Leviathan.Services.Core.Hardware;
using Leviathan.Services.Core.QuickStart;
using Leviathan.Services.DbInit.Npgsql;
using Leviathan.Services.Hardware.Npgsql.Modules;
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

namespace Leviathan.API.REST {
	public class Startup {

		protected ILeviathanCore Core;
		protected IHardwareService hardware;
		protected IEnumerable<IQuickStartProfile> quickStarts;

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {

			var pgConnectionString = "Host=poseidonalpha.local;Database=postgres;Username=pi;Password=Digital!2021;";
			var dbConnectionString = "Host=poseidonalpha.local;Database=Leviathan0x00;Username=pi;Password=Digital!2021;";

			services.AddSingleton(new CoreConfig { DbName = "Leviathan0x00" });
			services.AddSingleton<IDbInitService>(new DbInitService(pgConnectionString));
			services.AddSingleton<IDbConnectionProvider<NpgsqlConnection>>(new NpgsqlConnectionProvider(dbConnectionString));

			//services.AddSingleton<IEnumerable<IQuickStartProfile>>(});
			services.AddSingleton<IQuickStartService>(a => new QuickStartService(new[] {
				new RoboTankQuickStart(this.hardware)
			}));
			services.AddSingleton<IHardwareModuleTypeData, ModuleTypeRepo>();
			services.AddSingleton<IHardwareModuleData, ModuleData>();
			services.AddSingleton<IChannelTypeData, ChannelTypeRepo>();
			services.AddSingleton<IChannelData, ChannelData>();
			services.AddSingleton<IChannelControllerTypeData, ChannelControllerTypeRepo>();
			services.AddSingleton<IChannelControllerData, ChannelControllerRepo>();
			services.AddSingleton<IHardwareService, HardwareService>();

			services.AddSingleton<ILogger<LeviathanCore>>(new ConsoleLogger<LeviathanCore>());
			services.AddSingleton<ILeviathanCore, LeviathanCore>();

			services
				.AddControllers()
				.AddJsonOptions(options => {
					options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
					options.JsonSerializerOptions.IgnoreNullValues = true;
				});

			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.API.REST", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime) {
			lifetime.ApplicationStarted.Register(OnStart);
			lifetime.ApplicationStopping.Register(OnStop);

			//assign the core.
			this.hardware = app.ApplicationServices.GetService<IHardwareService>();
			this.Core = app.ApplicationServices.GetService<ILeviathanCore>();

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leviathan.API.REST v1"));
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

	public abstract class Logger<T> : ILogger<T> {

		public virtual IDisposable BeginScope<TState>(TState state) => state as IDisposable;

		public virtual bool IsEnabled(LogLevel logLevel) => true;

		public abstract void Log<TState1>(LogLevel logLevel, EventId eventId, TState1 state, Exception exception, Func<TState1, Exception, string> formatter);

	}

	public class ConsoleLogger<TState> : Logger<TState> {

		public override void Log<TState1>(LogLevel logLevel, EventId eventId, TState1 state, Exception exception, Func<TState1, Exception, string> formatter) =>
			Console.WriteLine($"{DateTimeOffset.Now} {logLevel}:{formatter(state, exception)}");
	}
}