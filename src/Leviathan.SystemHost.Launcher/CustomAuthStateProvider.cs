using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Leviathan.SystemHost.Launcher {
	public class CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor) : AuthenticationStateProvider {

		public override Task<AuthenticationState> GetAuthenticationStateAsync() {
			var cookieValue = httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];

			if (string.IsNullOrEmpty(cookieValue)) {
				return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
			}

			var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, cookieValue) }, "apiauth_type");
			var user = new ClaimsPrincipal(identity);

			return Task.FromResult(new AuthenticationState(user));
		}
	}
}