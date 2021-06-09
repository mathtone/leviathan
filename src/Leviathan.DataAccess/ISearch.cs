using System.Collections.Generic;

namespace Leviathan.DataAccess {
	public interface ISearch<S, T> {
		IEnumerable<T> Search(S criteria);
	}
}