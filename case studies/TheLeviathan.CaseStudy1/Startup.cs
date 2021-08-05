using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLeviathan.CaseStudy1
{

	public class ServiceCore
	{
		IHost _host;

		public async Task RunAsync(params string[] args) =>
			await (_host = Host.CreateDefaultBuilder()
				.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup(svc => this))
				.Build())
			.RunAsync();

		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddControllers(options => options.Conventions.Add(new ControllerRouteConvention()))
				.ConfigureApplicationPartManager(mgr => mgr.FeatureProviders.Add(new ControllerFeatureProvider()));

			//services.AddControllers();
			services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheLeviathan.CaseStudy1", Version = "v1" }));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheLeviathan.CaseStudy1 v1"));
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}

	public class ControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
	{
		public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
		{
			;
		}
	}

	public class ControllerRouteConvention : IControllerModelConvention
	{
		public void Apply(ControllerModel controller)
		{
			;
		}
	}

	public static class Program
	{
		static async Task Main(string[] args) => await new ServiceCore().RunAsync(args);
	}
}

//public class Startup
//{
//	public Startup(IConfiguration configuration)
//	{
//		Configuration = configuration;
//	}

//	public IConfiguration Configuration { get; }

//	// This method gets called by the runtime. Use this method to add services to the container.
//	public void ConfigureServices(IServiceCollection services)
//	{
//		services.AddControllers();
//		services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheLeviathan.CaseStudy1", Version = "v1" }));
//	}

//	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//	{
//		if (env.IsDevelopment())
//		{
//			app.UseDeveloperExceptionPage();
//		}

//		app.UseSwagger();
//		app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheLeviathan.CaseStudy1 v1"));
//		app.UseHttpsRedirection();
//		app.UseRouting();
//		app.UseAuthorization();
//		app.UseEndpoints(endpoints => endpoints.MapControllers());
//	}
//}

//public class Program
//{
//	public static void Main(string[] args)
//	{
//		CreateHostBuilder(args).Build().Run();
//	}

//	public static IHostBuilder CreateHostBuilder(string[] args) =>
//		Host.CreateDefaultBuilder(args)
//			.ConfigureWebHostDefaults(webBuilder =>
//			{
//				webBuilder.UseStartup<Startup>();
//			});
//}
