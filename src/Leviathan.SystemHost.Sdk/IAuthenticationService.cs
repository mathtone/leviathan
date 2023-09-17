using System.IdentityModel.Tokens.Jwt;

namespace Leviathan.SystemHost.Sdk {
	public interface IAuthenticationService {
		Task<LoginResponse> Login(LoginRequest request);
	}

	public class LoginRequest {
		public string Login { get; init; } = "";
		public string Password { get; init; } = "";
	}

	public class LoginResponse {
		public string? Token { get; init; }
		public bool Successful { get; init; }
	}
}
