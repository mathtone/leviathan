using System;
using System.Collections.Generic;

namespace Leviathan.Data {

	public abstract record RecordBase { }

	public abstract record RecordBase<ID> : RecordBase {
		public ID Id { get; init; }
	}

	public interface IRepository<T, ID> {
		void Create(T entity);
		T Read(ID id);
		void Update(T entity);
		void Delete(ID id);
	}

	public interface IListProvider<T> {
		IEnumerable<T> List();
	}

	public interface IListRepository<T, ID> : IRepository<T, ID>, IListProvider<T> {
	}
}