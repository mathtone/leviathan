using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Leviathan.Services.Core;
using Leviathan.Services.DbInit.Npgsql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Leviathan.API.REST {
	public class Startup {

		readonly LeviathanCore core;

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
			var config = new CoreConfig { DbName = "Leviathan0x00" };
			var dbInit = new DbInitService("Host=poseidonalpha.local;Database=postgres;Username=pi;Password=Digital!2021;");
			core = new LeviathanCore(config, dbInit, new ConsoleLogger<LeviathanCore>());
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddSingleton<ILeviathanCore>(core);
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

		protected void OnStart() => core.Start();
		protected void OnStop() => core.Stop();
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