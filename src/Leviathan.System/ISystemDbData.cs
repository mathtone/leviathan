namespace Leviathan.System {
	public interface ISystemDbData {
		void CreateDB(string name);
		void DropDB(string name);
		bool LocateDB(string name);
	}
}