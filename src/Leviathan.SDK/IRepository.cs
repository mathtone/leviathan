namespace Leviathan.SDK {
	public interface IRepository<ID, T> {
		ID Create(T item);
		T Read(ID id);
		void Update(T item);
		void Delete(ID id);
	}
}