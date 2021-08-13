namespace Leviathan.Common {
	public interface IRepository<ID, T> {
		ID Create(T item);
		T Read(ID id);
		void Update(T item);
		void Delete(ID id);
	}

	public abstract class Repository<ID, T> : IRepository<ID, T> {
		public abstract ID Create(T item);
		public abstract void Delete(ID id);
		public abstract T Read(ID id);
		public abstract void Update(T item);
	}

}