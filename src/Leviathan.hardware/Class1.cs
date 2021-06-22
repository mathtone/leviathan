using System;

namespace Leviathan.Hardware {
	public interface IInputChannel<out T> {
		T GetValue();
	}

	public interface IOutputChannel<in T> {
		void SetValue(T value);

	}
}
