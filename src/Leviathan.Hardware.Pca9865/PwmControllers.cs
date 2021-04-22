namespace Leviathan.Hardware.PCA9685 {
	public class PwmOnOffController : InputOutputChannelController<bool, PwmIOChannel, ChannelControllerConfig> {
		//public override PwmIOChannel Value { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public override bool Value {
			get => Channel.Value > 0;
			set => Channel.Value = value ? 1 : 0;
		}

		public PwmOnOffController(PwmIOChannel channel, ChannelControllerConfig config) : base(channel, config) {}
	}

	public class PwmVariableController : InputOutputChannelController<double, PwmIOChannel, ChannelControllerConfig>, IInputOutputChannel<double> {
		
		public override double Value {
			get => Channel.Value;
			set => Channel.Value = value;
		}

		public PwmVariableController(PwmIOChannel channel, ChannelControllerConfig config) : base(channel, config) {}
	}
}