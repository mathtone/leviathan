using Leviathan.SystemHost.Sdk;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;

namespace Leviathan.SystemHost.Services {
	public class AuthenticationService : IAuthenticationService {
		
		private ICryptoService _crypto;
		public JwtSecurityToken Token { get; private set; }

		public AuthenticationService(ICryptoService crypto) =>
			_crypto = crypto;

		public Task<LoginResponse> Login(LoginRequest request) {

			Token = _crypto.CreateToken(request.Login);

			return Task.FromResult(new LoginResponse {
				Token = new JwtSecurityTokenHandler().WriteToken(Token),
				Successful = true
			});
		}
	}
}