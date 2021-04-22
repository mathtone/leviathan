using System.Collections.Generic;

namespace Leviathan.DataAccess {
	public interface IListProvider<T> {
		IEnumerable<T> List();
	}
}
