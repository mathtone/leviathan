namespace Leviathan.Hardware {
	public interface IInputOutputChannel<T> : IChannel, IInputChannel<T>,IOutputChannel<T>{
		new T Value { get; set; }
	}
	public abstract class InputOutputChannel<T, D, C> : Channel<D,C>, IInputOutputChannel<T> where C : ChannelConfig {
		public abstract T Value { get; set; }
		public InputOutputChannel(D device,C config) : base(device,config) {
		}
	}
}