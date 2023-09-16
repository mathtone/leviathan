using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Leviathan.SystemHost.Api.Controllers {

	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase {

		readonly RSACryptoServiceProvider _crypto;

		public AuthenticationController() {
			_crypto = new RSACryptoServiceProvider(2048);
			_crypto.ImportPkcs8PrivateKey(new ReadOnlySpan<byte>(Convert.FromBase64String(GetKey())), out _);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel login) {
			if (login.Username != "test") {
				return Unauthorized();
			}
			else {
				try {
					return Ok(new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(login.Username)));
				}
				catch (Exception ex) {
					return StatusCode(StatusCodes.Status500InternalServerError, ex);
				}
			}
		}

		private JwtSecurityToken GenerateJwtToken(string username) {
			var claims = new Claim[] {
				new(JwtRegisteredClaimNames.Sub, username),
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			return new JwtSecurityToken(
				 claims: claims,
				 expires: DateTime.Now.AddDays(30),
				 signingCredentials: new SigningCredentials(new RsaSecurityKey(_crypto), SecurityAlgorithms.RsaSha256)
			);
		}

		static string GetKey() => System.IO.File
			.ReadAllText($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!}/leviathan_private_key.pem")
			.Replace("-----BEGIN PRIVATE KEY-----", "")
			.Replace("-----END PRIVATE KEY-----", "").Trim();
	}

   public class LoginModel {
      public string? Username { get; set; }
      public string? Password { get; set; }
   }
}