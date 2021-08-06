using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace TheLeviathan.Api {
	public class LeviathanHostStartup {

		public IConfiguration Configuration { get; }
		public ProgramConfigurationOptions ProgramConfig { get; }

		public LeviathanHostStartup(IConfiguration configuration) {
			Configuration = configuration;
			ProgramConfig = new ProgramConfigurationOptions();
			Configuration.GetSection(ProgramConfigurationOptions.SectionName).Bind(ProgramConfig);
		}

		public void ConfigureServices(IServiceCollection services) {

			foreach (var m in ProgramConfig.ServiceConfigurationMethods) {
				Type.GetType($"{m.TypeName}, {m.AssemblyName}")
					.GetMethod(m.MethodName)
					.Invoke(null, new[] { services });
			};
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger()
				.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leviathan.Api v1"))
				.UseHttpsRedirection()
				.UseRouting()
				.UseAuthorization()
				.UseEndpoints(endpoints => {
					endpoints.MapControllers();
				});
		}
	}
}