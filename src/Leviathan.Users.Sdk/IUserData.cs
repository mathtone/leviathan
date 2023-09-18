using Mathtone.Sdk.Patterns;

namespace Leviathan.Users {
	public interface IUserData : IAsyncListRepository<int, UserRecord> {
      Task<UserRecord?> ReadByLogin(string login);
   }

   public record UserRecord(long Id, string Login) {
      public string? PasswordHash { get; init; }
   }
}