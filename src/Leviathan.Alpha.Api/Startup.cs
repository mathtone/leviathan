using Leviathan.Alpha.Hardware;
using Leviathan.Hardware;
using Leviathan.Hardware.OneWire;
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
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api {
	public class Startup {
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddTheLeviathan()
				.AddControllers(o => o.Conventions.Add(new GenericControllerRouteConvention()))
				.ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()))
				.AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leviathan.Alpha.Api", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leviathan.Alpha.Api v1"));
			}
			app.AwakenTheLeviathan(env, lifetime);
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class GeneratedControllerAttribute : Attribute {
		public GeneratedControllerAttribute(string route) {
			Route = route;
		}

		public string Route { get; set; }
	}

	public class GenericControllerRouteConvention : IControllerModelConvention {
		public void Apply(ControllerModel controller) {
			if (controller.ControllerType.IsGenericType) {
				var genericType = controller.ControllerType.GenericTypeArguments[0];
				controller.ControllerName = "ABeak";
				controller.Selectors.Add(new SelectorModel {
					AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"api/{controller.ControllerName}/{genericType.Name}/{{id}}")),
				});
			}
		}
	}

	public class ChannelsController<T> : GenericController<T> {

		ILeviathanHardwareSystemService _service;

		public ChannelsController(ILeviathanHardwareSystemService service) {
			_service = service;
		}

		[HttpGet,Route("[action]"]
		public int[] List()=>_service.Channels.Where(c=>c is IAsyncOutputChannel<T> || c is IOutputChannel<T>).Select(c=>c)


		
		[HttpGet]
		public async Task<T> Get(long id) {
			await _service.Initialize;
			var c = _service.Channels[id];
			if (c is IAsyncInputChannel<T> cInputAsync) {
				return await cInputAsync.GetValue();
			}
			if (c is IInputChannel<T> cInput) {
				return cInput.GetValue();
			}
			else {
				throw new Exception($"Channel {id} cannot be read as type: {typeof(T).Name}");
			}
		}

		[HttpPost]
		public async Task Set(long id, T value) {
			await _service.Initialize;
			var c = _service.Channels[id];
			if (c is IAsyncOutputChannel<T> cInputAsync) {
				await cInputAsync.SetValue(value);
			}
			if (c is IOutputChannel<T> cInput) {
				cInput.SetValue(value);
			}
			else {
				throw new Exception($"Channel {id} cannot be read as type: {typeof(T).Name}");
			}
		}
	}

	public class GenericController<T> : ControllerBase {

		//[HttpGet]
		//public T Get(long id) =>
		//	default;
	}

	public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature> {

		public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) {

			var currentAssembly = typeof(GenericTypeControllerFeatureProvider).Assembly;
			var candidates = currentAssembly.GetExportedTypes()
				.Where(x => x.GetCustomAttributes<GeneratedControllerAttribute>().Any());

			feature.Controllers.Add(typeof(ChannelsController<bool>).GetTypeInfo());
			feature.Controllers.Add(typeof(ChannelsController<int>).GetTypeInfo());
			feature.Controllers.Add(typeof(ChannelsController<TempReading>).GetTypeInfo());
			feature.Controllers.Add(typeof(ChannelsController<double>).GetTypeInfo());
		}
	}
}