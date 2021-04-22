namespace Leviathan.Hardware {
	public interface IOutputChannel<T> : IChannel {
		T Value { set; }
	}
	public abstract class OutputChannel<T, D, C> : Channel<D, C>, IOutputChannel<T> where C : ChannelConfig {
		public abstract T Value { set; }
		public OutputChannel(D device, C config) : base(device, config) {
		}
	}
}