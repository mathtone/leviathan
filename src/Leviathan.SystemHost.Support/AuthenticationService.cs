using Leviathan.Security;
using Leviathan.Users;
using System.IdentityModel.Tokens.Jwt;

namespace Leviathan.SystemHost.Support {
	public class AuthenticationService(ICryptoService crypto, IUserData userData) : IAuthenticationService {
		public async Task<LoginResult> Login(LoginModel model) {
			var user = await userData.ReadByLogin(model.UserName);
			if (user != null && crypto.VerifyHash(model.Password, user.PasswordHash!)) {
				var token = crypto.CreateToken(user.Login);
				return new LoginResult {
					UserName = user.Login,
					Token = new JwtSecurityTokenHandler().WriteToken(token)
				};
			}
			return new();
		}

		public Task<LoginResult> Restore(string token) {
			var jtok = crypto.Decode(token);
			return Task.FromResult(new LoginResult {
				UserName = jtok?.Subject,
				Token = jtok == null ? null : token
			});
		}
	}
}