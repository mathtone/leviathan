namespace Leviathan.DataAccess {


	public interface IListRepository<T> : IListRepository<T, int> { }
	public interface IListRepository<T, ID> : IListProvider<T>, IRepository<T, ID> { }
}
