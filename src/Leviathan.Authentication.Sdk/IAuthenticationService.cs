using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Authentication {

	public interface IAuthenticationService {
		Task<LoginResult> Login(LoginModel model);
	}

	public class LoginModel {
		public string UserName { get; set; } = "";
		public string Password { get; set; } = "";
	}

	public class LoginResult {
		public string? UserName { get; set; }
		public string? Token { get; set; }
		public bool Success => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Token);
	}
}