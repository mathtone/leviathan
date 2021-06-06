namespace Leviathan.DataAccess {
	public interface IRepository<T> : IRepository<T, int> { }
	public interface IRepository<T, ID> {
		T Create(T item);
		T Read(ID id);
		T Update(T item);
		void Delete(ID itemId);
	}
}