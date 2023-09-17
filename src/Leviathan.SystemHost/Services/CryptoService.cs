using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Leviathan.SystemHost.Services {

	public class CryptoService : ICryptoService {

		readonly RSACryptoServiceProvider _rsa = new(2048);

		public CryptoService(IKeyProvider keyProvider) {
			var str = keyProvider.GetKey("leviathan_private_key")!;
			var bytes = new ReadOnlySpan<byte>(Convert.FromBase64String(str));
			_rsa.ImportPkcs8PrivateKey(bytes, out _);
		}

		public JwtSecurityToken CreateToken(string userName) {
			var claims = new Claim[] {
				new(JwtRegisteredClaimNames.Sub, userName),
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			return new JwtSecurityToken(
				 claims: claims,
				 expires: DateTime.Now.AddDays(30),
				 signingCredentials: new SigningCredentials(new RsaSecurityKey(_rsa), SecurityAlgorithms.RsaSha256)
			);
		}
	}

	public interface ICryptoService {
		JwtSecurityToken CreateToken(string userName);
	}
}