using System.IdentityModel.Tokens.Jwt;

namespace Leviathan.Crypto {
	public interface ICryptoService {
		public JwtSecurityToken CreateToken(string userName);
	}

	public interface IKeyProvider {
		public string GetKey(string name);
		public ReadOnlySpan<byte> GetKeyBytes(string name);
	}
}