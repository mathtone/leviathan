using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using IoFile =  System.IO.File;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Leviathan.System.Api.Controllers {

	[ApiController]
	[Route("[controller]/[action]")]
	public class AuthenticationController : ControllerBase {

		readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

		public IConfiguration Configuration { get; }

		public AuthenticationController(IConfiguration configuration) {
			Configuration = configuration;
		}

		[HttpPost]
		public async Task<IActionResult> Login(string login, string password) {
			if (login != "test") {
				return Unauthorized();
			}
			else {
				return Ok(new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(login)));
			}
		}

		private JwtSecurityToken GenerateJwtToken(string username) {
			// Load the private key
			
			var privateKey = IoFile.ReadAllText("path_to_private_key.pem");
			var rsa = new RSACryptoServiceProvider();
			rsa.ImportPkcs8PrivateKey(new ReadOnlySpan<byte>(Convert.FromBase64String(privateKey)), out _);

			var credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

			var claims = new[]
			{
		  new Claim(JwtRegisteredClaimNames.Sub, username),
		  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
	 };

			return new JwtSecurityToken(
				 claims: claims,
				 expires: DateTime.Now.AddHours(3), // Token expiration, modify as needed
				 signingCredentials: credentials
			);
		}

		[HttpGet]
		public Task<string> CreateKey() {

			var key = new byte[32];
			_rng.GetBytes(key);
			var base64Secret = Convert.ToBase64String(key);
			var urlEncoded = base64Secret.TrimEnd('=').Replace('+', '-').Replace('/', '_');

			return Task.FromResult(urlEncoded);
		}
	}
}