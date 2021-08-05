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

namespace CaseStudy.DynamicApi
{

	public class Startup
	{

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddControllers(options => options.Conventions.Add(new ControllerRouteConvention()))
				.ConfigureApplicationPartManager(mgr => mgr.FeatureProviders.Add(new ControllerFeatureProvider()));

			services.AddSwaggerGen(c =>
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "CaseStudy.DynamicApi", Version = "v1" })
			);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger()
				.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CaseStudy.DynamicApi v1"))
				.UseHttpsRedirection()
				.UseRouting()
				.UseAuthorization()
				.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}

	public class ControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
	{
		public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
		{

			//var currentAssembly = typeof(GenericTypeControllerFeatureProvider).Assembly;
			//var candidates = currentAssembly.GetExportedTypes().Where(x => x.GetCustomAttributes<GeneratedControllerAttribute>().Any());

			//foreach (var candidate in candidates)
			//{
			//	feature.Controllers.Add(
			//		typeof(BaseController<>).MakeGenericType(candidate).GetTypeInfo()
			//	);
			//}
		}
	}

	public class ControllerRouteConvention : IControllerModelConvention
	{
		public void Apply(ControllerModel controller)
		{
			//controller.Selectors.Clear();
			//controller.Selectors.Add(new SelectorModel
			//{
			//	AttributeRouteModel = new AttributeRouteModel(new RouteAttribute("BEEXNEEK")),
			//});

			//if (controller.ControllerType.IsGenericType)
			//{
			//	var genericType = controller.ControllerType.GenericTypeArguments[0];
			//	var customNameAttribute = genericType.GetCustomAttribute<GeneratedControllerAttribute>();

			//	if (customNameAttribute?.Route != null)
			//	{
			//		controller.Selectors.Add(new SelectorModel
			//		{
			//			AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(customNameAttribute.Route)),
			//		});
			//	}
			//	else
			//	{
			//		controller.ControllerName = genericType.Name;
			//	}
			//}
		}
	}
}