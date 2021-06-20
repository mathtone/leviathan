﻿using Leviathan.Alpha.Components;
using Leviathan.Alpha.Core;
using Leviathan.Alpha.Startup;
using Leviathan.SDK;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api {


	public static class ServiceConfiguration {
		public static IServiceCollection AddTheLeviathan(this IServiceCollection services) => services
			.AddSingleton<IAmLeviathan, TheLeviathan>()
			.AddSingleton<ILeviathanSystem, LeviathanSystem>()
			.AddSingleton<IStartupService, StartupService>()
			.AddSingleton<IComponentsService, ComponentsService>();

		public static IApplicationBuilder AwakenTheLeviathan(this IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime) {
			var leviathan = app.ApplicationServices.GetService<IAmLeviathan>();
			lifetime.ApplicationStarted.Register(async () => await leviathan.Start(new LeviathanHostEnvironment(env, lifetime)));
			lifetime.ApplicationStopping.Register(async () => await leviathan.Stop());
			return app;
		}
	}
}