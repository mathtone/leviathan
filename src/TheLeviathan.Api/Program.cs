using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheLeviathan.Api {
	public class Program {
		public static void Main(string[] args) =>
			CreateHostBuilder(args).Build().Run();

		public static IHostBuilder CreateHostBuilder(string[] args) => Host
			.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder => {
				webBuilder.UseStartup<LeviathanHostStartup>();
			});
	}
}