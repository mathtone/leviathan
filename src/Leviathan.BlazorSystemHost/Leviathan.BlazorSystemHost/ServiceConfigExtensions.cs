using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Leviathan.SystemHost.Support;
using Leviathan.Security;
using System.Reflection;
using Leviathan.Services;
using Leviathan.Data;
using Leviathan.Users;
using Npgsql;
namespace Leviathan.BlazorSystemHost {
	public static class ServiceConfigExtensions {

		public static IServiceCollection ConfigureAll(this IServiceCollection services) => services
			.AddConfiguration()
			.AddBlazoriseServices()
			.AddSystemServices();



		public static IServiceCollection AddBlazoriseServices(this IServiceCollection services) => services
			.AddBlazorise()
			.AddBootstrap5Providers()
			.AddFontAwesomeIcons();

		public static IServiceCollection AddConfiguration(this IServiceCollection services) => services
			.ConfigureSection<NpgSqlConnectionProviderConfig>("NpgsqlConnectionProvider");

		public static IServiceCollection AddSystemServices(this IServiceCollection services) => services
			.AddHttpContextAccessor()
			.AddKeys(new() {
				{"leviathan_private_key", GetKeyFromFile }
			})
			.AddSingleton<ICryptoService, SystemCryptoService>()
			.AddSingleton<IConnectionProvider<NpgsqlConnection>, NpgsqlConnectionProvider>()
			.AddSingleton<IAuthenticationService, AuthenticationService>()
			.AddSingleton<IUserData, NpgsqlUserData>()

			.AddScoped<ICurrentUser, CurrentUserService>();


		static string GetKeyFromFile(string name) => File
			.ReadAllText($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!}/../../../../../../{name}.pem")
			.Replace("-----BEGIN PRIVATE KEY-----", "")
			.Replace("-----END PRIVATE KEY-----", "")
			.Trim();
	}
}