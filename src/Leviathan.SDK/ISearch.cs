using System;
using System.Collections;
using System.Collections.Generic;

namespace Leviathan.SDK {

	public interface ISearch<S, T> {
		IEnumerable<T> Search(S criteria);
	}
}