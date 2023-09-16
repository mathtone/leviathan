using static System.Net.WebRequestMethods;

namespace Leviathan.InstanceHost.Ui.Services {
	public class AuthenticationService : IAuthenticationService {
		readonly HttpClient _httpClient;
		readonly AuthenticationServiceConfig _config;
		string? _token = default;

		public string? Token => _token;
		
		public AuthenticationService(AuthenticationServiceConfig config, HttpClient httpClient) {
			_httpClient = httpClient;
			_config = config;
		}

		public async Task<LoginResult> Login(LoginModel login) {

			var response = await _httpClient.PostAsJsonAsync(_config.Url, login);
			if (response.IsSuccessStatusCode) {
				_token = await response.Content.ReadAsStringAsync();
				return new LoginResult() {
					Successfull = response.IsSuccessStatusCode
				};
			}
			return new LoginResult() {
				Successfull = response.IsSuccessStatusCode
			};
		}
	}

	public interface IAuthenticationService {
		public Task<LoginResult> Login(LoginModel login);
	}

	public class LoginModel {
		public string? Username { get; set; }
		public string? Password { get; set; }
	}

	public class LoginResult {
		public bool Successfull { get; set; }
	}

	public class AuthenticationServiceConfig {
		public string Url = "https://localhost:7249/api/Authentication";
	}
}

