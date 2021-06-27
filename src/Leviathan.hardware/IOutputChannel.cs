using System;

namespace Leviathan.Hardware {

	public interface IOutputChannel<in T> {
		void SetValue(T value);

	}
}
