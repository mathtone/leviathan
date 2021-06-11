using System.Collections.Generic;

namespace Leviathan.DataAccess {
	public interface IListRepository<T> : IListRepository<long, T> { }
	public interface IListRepository<ID, T> : IListRepository<ID, T, T> { }
	public interface IListRepository<ID, T, C> : IRepository<T, ID> {
		IEnumerable<C> List();
	}
}