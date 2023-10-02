using Blazorise;
using Leviathan.BlazorSystemHost;
using Leviathan.Services;
using Leviathan.SystemHost;
using Leviathan.SystemHost.Support;
using Leviathan.SystemHost.Updater;
using Microsoft.Extensions.DependencyInjection;

if (args.Length > 0 && args[0] == "update") {

	var cfg = new ConfigurationBuilder()
		.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
		.AddEnvironmentVariables()
		.Build();

#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
	await new ServiceCollection()
		.ConfigureSection<NpgSqlConnectionProviderConfig>("NpgsqlConnectionProvider")
		.AddSingleton<IConfiguration>(cfg)
		.AddTransient<UpgradeService>()
		.BuildServiceProvider()
		.GetRequiredService<UpgradeService>()
		.Update();
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.ConfigureAll().AddSingleton(new SystemHostConfig {
	IsInitialized = true
});

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