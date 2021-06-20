using Leviathan.SDK;

namespace Leviathan.DbDataAccess {
	public abstract class DataRepository<ID, T> : IRepository<ID, T> {
		public abstract ID Create(T item);
		public abstract void Delete(ID id);
		public abstract T Read(ID id);
		public abstract void Update(T item);
	}
}