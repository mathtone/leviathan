using System.Collections.Generic;

namespace Leviathan.SDK {
	public interface IListing<T> {
		IEnumerable<T> List();
	}
}