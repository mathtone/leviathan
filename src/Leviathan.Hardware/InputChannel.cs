namespace Leviathan.Hardware {
	public interface IInputChannel<T> : IChannel {
		T Value { get; }
	}
	public abstract class InputChannel<T, D, C> : Channel<D, C>, IInputChannel<T> where C : ChannelConfig {

		public abstract T Value { get; }

		public InputChannel(D device, C config) : base(device, config) {
		}
	}
}