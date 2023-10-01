using Mathtone.Sdk.Patterns;

namespace Leviathan.Users {
	public interface IUserData : IAsyncListRepository<int, UserRecord> {
		Task<UserRecord?> ReadByLogin(string login);
	}
}
