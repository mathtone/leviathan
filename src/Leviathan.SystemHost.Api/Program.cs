using Leviathan.SystemHost.Sdk;
using Leviathan.SystemHost.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddSingleton<IAuthenticationService, AuthenticationService>()
	.AddSingleton<ICryptoService, CryptoService>()
	.AddSingleton<IKeyProvider, FileKeyProvider>()
	.AddHostedService<SystemHostService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
