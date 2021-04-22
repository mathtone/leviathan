namespace Leviathan.DataAccess {
	//public class DataRepository<T,ID> : IRepository<T, ID> { }
	public interface IExtendedRepository<T, ID, S> : IListRepository<T, ID>, ISearchable<T, S> { }
}
