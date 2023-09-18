namespace Leviathan.SystemHost.Launcher {

	public static class LeviathanSystemHostBuilder {

		public static WebApplicationBuilder CreateDefault() {
			var builder = WebApplication.CreateBuilder();
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
