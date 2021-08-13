using Leviathan.Common;
using System.Threading.Tasks;

namespace Leviathan.DataAccess {
	public interface IAsyncRepository<ID, T> {
		Task<ID> CreateAsync(T item);
		Task<T> ReadAsync(ID id);
		Task UpdateAsync(T item);
		Task DeleteAsync(ID id);
	}

	public abstract class AsyncRepository<ID, T> : Repository<ID, T>, IAsyncRepository<ID, T> {

		public override ID Create(T item) => CreateAsync(item).Result;
		public override T Read(ID id) => ReadAsync(id).Result;
		public override void Update(T item) => UpdateAsync(item).Wait();
		public override void Delete(ID id) => DeleteAsync(id).Wait();

		public abstract Task<ID> CreateAsync(T item);
		public abstract Task<T> ReadAsync(ID id);
		public abstract Task DeleteAsync(ID id);
		public abstract Task UpdateAsync(T item);
	}
}