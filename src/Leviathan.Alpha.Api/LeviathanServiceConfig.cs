using Leviathan.Core.SDK;
using Leviathan.Services.SDK;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Leviathan.Alpha.Api {
	public static class LeviathanServiceConfig {

		//public static IServiceCollection AddTheLeviathan(this IServiceCollection services) => services
		//	.AddModularServices();

		public static IApplicationBuilder AwakenTheLeviathan(this IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime) {

			var leviathan = app.ApplicationServices.GetService<ITheLeviathan>();
			lifetime.ApplicationStarted.Register(async () => await leviathan.Start());
			lifetime.ApplicationStopping.Register(async () => await leviathan.Stop());
			return app;
		}
	}
}