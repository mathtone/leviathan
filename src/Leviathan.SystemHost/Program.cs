using Leviathan.Data;
using Leviathan.Users;
using Npgsql;
using System.Data.Common;
using System.Data;
using Leviathan.Security;
using System.Reflection;
using Leviathan.Services;
using Blazorise;
using Leviathan.SystemHost.Support;

//Perform System Init Check

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services
	.AddBlazorise(options => {
		options.Immediate = true;
	})
	.ConfigureSection<NpgSqlConnectionProviderConfig>("NpgsqlConnectionProvider")
	.AddHttpContextAccessor()
	.AddKeys(new() {
		{"leviathan_private_key", GetKeyFromFile }
	})
	.AddSingleton<ICryptoService, SystemCryptoService>()
	.AddSingleton<IConnectionProvider<NpgsqlConnection>, NpgsqlConnectionProvider>()
	.AddSingleton<IAuthenticationService, AuthenticationService>()
	.AddSingleton<IUserData, NpgsqlUserData>()
	.AddHostedService<SystemHostService>();

var app = builder.Build();
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();

static string GetKeyFromFile(string name) => File
	.ReadAllText($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!}/../../../../../{name}.pem")
	.Replace("-----BEGIN PRIVATE KEY-----", "")
	.Replace("-----END PRIVATE KEY-----", "")
	.Trim();

class SystemHostService : IHostedService {
	public Task StartAsync(CancellationToken cancellationToken) {
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken) {
		return Task.CompletedTask;
	}
}