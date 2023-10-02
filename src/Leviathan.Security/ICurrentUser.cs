namespace Leviathan.Security {
	public interface ICurrentUser {
		bool LoggedIn { get; }
		string? Username { get; }
		Task<LoginResult> Login(string username, string password);
		Task<LoginResult> Restore(string token);
		Task Logout();
	}
}
