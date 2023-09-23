using Leviathan.Authentication;
using Leviathan.Crypto;
using System.Reflection;
using Leviathan.SystemHost.Launcher;
using Leviathan.Users.Npgsql;
using Leviathan.Users;
using Npgsql;
using Leviathan.Data.Npgsql;
using Leviathan.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Leviathan.SystemHostLauncher;
using Leviathan.SystemHost.Launcher.Services;
using Leviathan.SystemHost.Launcher.Authentication;
using Leviathan.InstanceHost;
using System.Runtime.InteropServices;


var app = LeviathanSystemHostBuilder
	.CreateDefault(args)
	.ConfigureServices(svc => svc
		.AddHttpContextAccessor()
		.AddKeys(new() {
				{"leviathan_private_key", GetKeyFromFile }
		})

		.ConfigureSection<NpgSqlConnectionProviderConfig>("NpgsqlConnectionProvider")

		.AddSingleton<IAuthenticationService, AuthenticationService>()
		.AddSingleton<IConnectionProvider<NpgsqlConnection>, NpgsqlConnectionProvider>()
		.AddSingleton<IUserData, NpgsqlUserData>()
		.AddSingleton<ICryptoService, SystemCryptoService>()

		.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>()

		.AddHostedService<InstanceHostService>()

	)
	.Build();

if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
await app.RunAsync();

static string GetKeyFromFile(string name) => File
	.ReadAllText($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!}/{name}.pem")
	.Replace("-----BEGIN PRIVATE KEY-----", "")
	.Replace("-----END PRIVATE KEY-----", "")
	.Trim();

public class SystemConfiguration {
	public bool IsInitialized { get; set; }
}