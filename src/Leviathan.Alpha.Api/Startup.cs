using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Leviathan.Alpha.Api {
	public class Startup {
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services) {
			services.AddTheLeviathan()
				.AddControllers(o => o.Conventions.Add(new ModularControllerRouteConvention()))
				.AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.Alpha.Api", Version = "v1" });
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leviathan.Alpha.Api v1"));
			}

			app.AwakenTheLeviathan(env, lifetime)
				.UseHttpsRedirection()
				.UseRouting()
				.UseAuthorization()
				.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
