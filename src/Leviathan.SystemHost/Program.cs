using Leviathan.Data.Npgsql;
using Leviathan.SystemHost.Data;
using Leviathan.SystemHost.Services;
using Mathtone.Sdk.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services
	.AddConfiguration<SystemHostServiceConfig>("SystemHostServiceConfig")
	.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>()
	.AddSingleton<ISystemDbConnectionProvider>(svc => new SystemDbNpgSqlConnectionProvider(svc.GetRequiredService<IConnectionStringProvider>()["SystemDb"]))
	.AddSingleton<ISystemDataService,SystemDataService >()
	.AddHostedService<SystemHostService>();

var app = builder.Build();
;
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
