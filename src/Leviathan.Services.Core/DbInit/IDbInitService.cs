namespace Leviathan.Services.Core {
	public interface IDbInitService {
		bool LocateDatabase(string name);
		void CreateDatabase(string name);
		void VerifyDatabase(string name);
		void ResetDatabase(string name);
	}
}