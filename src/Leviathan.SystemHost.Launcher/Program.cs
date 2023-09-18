using Leviathan.Authentication;
using Leviathan.Crypto;
using System.Reflection;
using Leviathan.SystemHost.Launcher;
using System.Security.Cryptography;
using Leviathan.Users.Npgsql;
using Leviathan.Users;
using Npgsql;
using Leviathan.Data.Npgsql;
using Leviathan.Data;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = LeviathanSystemHostBuilder
   .CreateDefault();

//builder.Services.AddAuthentication(options => {
//	options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//	options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//.AddCookie();

//builder.Services.AddAuthorization(options => {
//	options.FallbackPolicy = options.DefaultPolicy;
//});

var app = builder.ConfigureServices(svc => svc
   .AddKeys(new() {
         {"leviathan_private_key",GetKeyFromFile }
   })
   //.AddAuthorization(opt=>opt.FallbackPolicy  = opt.DefaultPolicy)
   .ConfigureSection<NpgSqlConnectionProviderConfig>("NpgsqlConnectionProvider")
   .AddSingleton<IAuthenticationService, AuthenticationService>()
   .AddSingleton<IConnectionProvider<NpgsqlConnection>, NpgsqlConnectionProvider>()
   .AddSingleton<IUserData, NpgsqlUserData>()
   .AddSingleton<ICryptoService, SystemCryptoService>()
   .AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>()
   .AddHttpContextAccessor()
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

class SystemCryptoService(IKeyProvider keyProvider) : CryptoServicebase(keyProvider) {
   protected override RSACryptoServiceProvider CreateServiceProvider() {
      var rtn = base.CreateServiceProvider();
      rtn.ImportPkcs8PrivateKey(KeyProvider.GetKeyBytes("leviathan_private_key"), out _);
      return rtn;
   }
}

public static class ServiceCollectionExtensions {
   public static IServiceCollection ConfigureSection<T>(this IServiceCollection services, string? sectionName = default)  where T : class,new() {
      return services.AddSingleton(svc => {
         var cfg = svc.GetRequiredService<IConfiguration>().GetSection(sectionName ?? typeof(T).Name);
         var rtn = new T();
         cfg.Bind(rtn);
         return rtn;
      });
   }
}


public class CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor) : AuthenticationStateProvider {

   public override Task<AuthenticationState> GetAuthenticationStateAsync() {
      var cookieValue = httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];

      // If there's no cookie, or it's empty, return an unauthenticated state.
      if (string.IsNullOrEmpty(cookieValue)) {
         return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
      }

      // If there's a valid token in the cookie, return an authenticated state.
      var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, cookieValue) }, "apiauth_type");
      var user = new ClaimsPrincipal(identity);

      return Task.FromResult(new AuthenticationState(user));
   }

}
