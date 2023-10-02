using Leviathan.Security;

namespace Leviathan.SystemHost.Support {
	public class CurrentUserService : ICurrentUser {

		readonly IAuthenticationService _authentication;

		public bool LoggedIn { get; protected set; }
		public string? Username { get; protected set; }

		public CurrentUserService(IAuthenticationService authentication) =>
			(_authentication) = (authentication);

		public async Task<LoginResult> Login(string username, string password) {
			var rslt = await _authentication.Login(
				new() { UserName = username, Password = password }
			);
			if (rslt.Success) {
				LoggedIn = true;
				Username = username;
			}
			return rslt;
		}

		public Task Logout() {
			LoggedIn = false;
			Username = null;
			return Task.CompletedTask;
		}

		public async Task<LoginResult> Restore(string token) {
			var rslt = await _authentication.Restore(token);
			if (rslt.Success) {
				LoggedIn = true;
				Username = rslt.UserName;
			}
			return rslt;
		}
	}
}