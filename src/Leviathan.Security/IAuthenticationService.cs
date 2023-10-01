using System.IdentityModel.Tokens.Jwt;

namespace Leviathan.Security {
	public interface IAuthenticationService {
		Task<LoginResult> Login(LoginModel model);
		Task<LoginResult> Restore(string token);
	}
}
