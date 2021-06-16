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

namespace Leviathan.Alpha.Api {
	public class Startup {

		public IConfiguration Configuration { get; }
		protected ILeviathan TheLeviathan { get; set; }

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
			//var lev = configurationGetValue<SystemConfiguration>("Leviathan");
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			//var config = new SystemConfiguration();
			//Configuration.GetSection("TheLeviathan").Bind(config);
			//services.AddSingleton(config);
			//services.Configure<SystemConfiguration>(x => Configuration.GetSection("TheLeviathan").Bind(x));
			//services.Configure<SystemConfiguration>(Configuration.GetSection("TheLeviathan"));

			services.LeviathanWakes();
			services.AddControllers();
			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.Alpha.Api", Version = "v1" });
			});
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
}