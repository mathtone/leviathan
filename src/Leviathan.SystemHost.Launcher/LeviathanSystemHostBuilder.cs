namespace Leviathan.SystemHost.Launcher {

	public static class LeviathanSystemHostBuilder {

		public static WebApplicationBuilder CreateDefault(string[] args) {
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();
			builder.Services.AddControllers();
			return builder;
		}

		public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder, Func<IServiceCollection, IServiceCollection> configAction) {
			configAction(builder.Services);
			return builder;
		}
	}
}
