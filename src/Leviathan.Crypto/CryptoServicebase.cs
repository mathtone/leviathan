using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Leviathan.Crypto {
	public abstract class CryptoServicebase(IKeyProvider keyProvider) : ICryptoService {

		RSACryptoServiceProvider? _rsa;

		protected IKeyProvider KeyProvider { get; } = keyProvider;
		protected RSACryptoServiceProvider RSA => _rsa ??= CreateServiceProvider();

		protected virtual RSACryptoServiceProvider CreateServiceProvider() {
			var rtn = new RSACryptoServiceProvider(2048);
			return rtn;
		}

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
	}
}