using System.Collections.Generic;

namespace Leviathan.DataAccess {
	public interface ISearchable<T, S> {
		public IEnumerable<T> Search(S search);
	}
}