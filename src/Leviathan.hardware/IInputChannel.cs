namespace Leviathan.Hardware {
	public interface IInputChannel<out T> : IChannel {
		T GetValue();
	}

	public interface IChannel { }
}
