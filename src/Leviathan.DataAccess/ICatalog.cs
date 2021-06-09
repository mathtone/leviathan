using System.Collections.Generic;

namespace Leviathan.DataAccess {
	public interface ICatalog<T> {
		IEnumerable<T> Catalog();
	}
}