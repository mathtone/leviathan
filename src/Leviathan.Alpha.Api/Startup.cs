using Leviathan.Alpha.Core;
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

namespace Leviathan.Api.Alpha {

	public enum ServiceLifetime {
		Singleton, Transient, Scoped, Hosted
	}

	public class Program {
		public static void Main(string[] args) => Host
			.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
			.Build().Run();
	}

	public class Startup {

		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration) =>
			Configuration = configuration;

		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers();
			services.AddSwaggerGen(c =>
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.Api.Alpha", Version = "v1" })
			);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseSwagger()
				.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leviathan.Api.Alpha v1"))
				.UseHttpsRedirection()
				.UseRouting()
				.UseAuthorization()
				.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}