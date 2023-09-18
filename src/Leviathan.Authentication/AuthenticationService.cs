using Leviathan.Crypto;
using Leviathan.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Authentication {
	public class AuthenticationService(ICryptoService crypto, IUserData userData) : IAuthenticationService {
		public async Task<LoginResult> Login(LoginModel model) {

			var user = await userData.ReadByLogin(model.UserName);
			if(user != null) {
				if(BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash)) {
					var token = crypto.CreateToken(user.Login);
					return new LoginResult {
						UserName = user.Login,
						Token = new JwtSecurityTokenHandler().WriteToken(token)
					};
				}
         }
			return new();
		}
	}
}