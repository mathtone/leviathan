namespace Leviathan.Users {
	public record UserRecord(long Id, string Login) {
		public string? PasswordHash { get; init; }
	}
}
