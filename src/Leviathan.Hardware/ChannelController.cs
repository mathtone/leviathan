using System;

namespace Leviathan.Hardware {
	public interface IChannelController {

	}

	public class ChannelControllerConfig {
		public int Id { get; set; }
		public string Name { get; set; }
		public int ChannelId { get; set; }
		public Type ControllerType { get; set; }
	}

	public class ChannelControllerInfo {
		public int Id { get; set; }
		public int ControllerTypeId { get; set; }
		public int ChannelId { get; set; }
		public string Name { get; set; }

		public ChannelControllerInfo() { }
		public ChannelControllerInfo(string name, int controllerTypeId,int channelId, int id = default) {
			this.Id = id;
			this.ControllerTypeId = controllerTypeId;
			this.ChannelId = channelId;
			this.Name = name;
		}
	}

	public class ChannelControllerTypeInfo {
		public int Id { get; set; }
		public string Name { get; set; }
		public string TypeInfo { get; set; }

		public ChannelControllerTypeInfo() { }
		public ChannelControllerTypeInfo(string typeInfo, string name, int id = default) {
			this.Id = id;
			this.Name = name;
			this.TypeInfo= typeInfo;
		}
	}

	public abstract class ChannelController<CH, CFG> : IChannel, IChannelController where CFG : ChannelControllerConfig {
		protected CH Channel { get; }
		protected CFG Config { get; }

		public int Id => Config.Id;
		public string Name => Config.Name;

		public ChannelController(CH channel, CFG config) {
			this.Channel = channel;
			this.Config = config;
		}
	}

	public abstract class InputChannelController<T, C, CFG> : ChannelController<C, CFG>, IInputChannel<T> where CFG : ChannelControllerConfig {
		public abstract T Value { get; }
		protected InputChannelController(C channel, CFG config) : base(channel, config) {
		}
	}

	public abstract class OutputChannelController<T, C, CFG> : ChannelController<C, CFG>, IOutputChannel<T> where CFG : ChannelControllerConfig {
		public abstract T Value { set; }
		protected OutputChannelController(C channel, CFG config) : base(channel, config) {
		}
	}

	public abstract class InputOutputChannelController<T, C, CFG> : ChannelController<C, CFG>, IInputOutputChannel<T> where CFG : ChannelControllerConfig {
		public abstract T Value { get; set; }
		protected InputOutputChannelController(C channel, CFG config) : base(channel, config) {
		}
	}
}