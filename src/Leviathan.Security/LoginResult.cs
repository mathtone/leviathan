using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Leviathan.Security {

	public class LoginResult {
		public string? UserName { get; set; }
		public string? Token { get; set; }
		public bool Success => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Token);
	}

	public abstract class CryptoServiceBase : ICryptoService {

		RSACryptoServiceProvider? _rsa;
		protected IKeyProvider KeyProvider { get; }
		protected RSACryptoServiceProvider RSA => _rsa ??= CreateServiceProvider();

		protected CryptoServiceBase(IKeyProvider keyProvider) {
			KeyProvider = keyProvider;
		}

		protected virtual RSACryptoServiceProvider CreateServiceProvider() =>
			new(2048);

		public virtual JwtSecurityToken CreateToken(string userName) {

			var claims = new Claim[] {
				new(JwtRegisteredClaimNames.Sub, userName),
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			return new(
				claims: claims,
				expires: DateTime.Now.AddDays(30),
					 signingCredentials: new SigningCredentials(new RsaSecurityKey(RSA), SecurityAlgorithms.RsaSha256)
			);
		}

		public virtual bool VerifyHash(string password, string hash) =>
			BCrypt.Net.BCrypt.Verify(password, hash);

		public JwtSecurityToken? Decode(string token) {
			var tokenHandler = new JwtSecurityTokenHandler();
			var validationParameters = new TokenValidationParameters {
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new RsaSecurityKey(RSA), // This should be your public key
				ValidateIssuer = false,
				ValidateAudience = false
			};

			try {
				tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
				return validatedToken as JwtSecurityToken;
			}
			catch {
				return null;
			}
		}
	}
}