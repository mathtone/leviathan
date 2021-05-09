using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Leviathan.UI.Alpha {
	public class Program {
		public static void Main(string[] args) {
			var d = Directory.GetCurrentDirectory() + "/wwwroot";
			var builder = CreateHostBuilder(args);
			if (!Directory.Exists(d)) { throw new Exception(); }
			var host = builder.Build();
			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder => {
				webBuilder.UseStartup<Startup>();
			});
	}
}