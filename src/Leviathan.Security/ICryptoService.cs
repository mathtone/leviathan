using System.IdentityModel.Tokens.Jwt;

namespace Leviathan.Security{
	public interface ICryptoService {
		public bool VerifyHash(string password, string hash);
		public JwtSecurityToken CreateToken(string userName);
		public JwtSecurityToken? Decode(string token);
	}
}
