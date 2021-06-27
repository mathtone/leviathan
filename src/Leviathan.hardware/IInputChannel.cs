namespace Leviathan.Hardware {
	public interface IInputChannel<out T> {
		T GetValue();
	}
}
