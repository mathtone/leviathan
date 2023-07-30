using Leviathan.Data.Npgsql;
using Leviathan.SystemHost.Data;
using Leviathan.SystemHost.Services;
using Mathtone.Sdk.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

if (args.Length != 0) {
	var delay = Convert.ToInt32(args[0]);
	Console.WriteLine($"Delaying startup for {delay}ms");
	Thread.Sleep(delay);
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthentication(options => {
	options.DefaultScheme = "CustomScheme";
})
.AddScheme<CustomAuthenticationHandlerOptions, CustomAuthenticationHandler>("CustomScheme", null);
builder.Services

	.AddConfiguration<SystemHostServiceConfig>("SystemHostServiceConfig")
	.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>()
	.AddSingleton<ISystemDbConnectionProvider>(svc => new SystemDbNpgSqlConnectionProvider(svc.GetRequiredService<IConnectionStringProvider>()["SystemDb"]))
	.AddSingleton<IUserService, UserService>()
	.AddSingleton<ISystemDataService, SystemDataService>()
	.AddSingleton<ISystemHostService, SystemHostService>()

	.AddTransient(svc => (IHostedService)svc.GetRequiredService<ISystemHostService>());

var app = builder.Build();
;
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();

public class CustomAuthenticationHandlerOptions : AuthenticationSchemeOptions {
	public string Scheme { get; } = "CustomScheme";
}

public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationHandlerOptions> {

	private readonly IUserService _userService;
	public CustomAuthenticationHandler(IOptionsMonitor<CustomAuthenticationHandlerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserService userService) :
		base(options, logger, encoder, clock) {
		_userService = userService;
	}
	protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
		string token = null;
		if (Request.Headers.ContainsKey("Authorization")) {
			string headerValue = Request.Headers["Authorization"];
			if (headerValue.StartsWith("Bearer ")) {
				token = headerValue.Substring("Bearer ".Length);
			}
		}
		if (string.IsNullOrEmpty(token)) {
			return AuthenticateResult.Fail("Invalid token");
		}
		ClaimsIdentity identity = null;
		// TODO: Validate token and create claims identity
		if (identity == null) {
			return AuthenticateResult.Fail("Invalid token");
		}
		AuthenticationTicket ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Options.Scheme);
		return AuthenticateResult.Success(ticket);
	}
}