using Leviathan.DataAccess;

namespace Leviathan.Hardware {



	public interface IConnectorData : IListRepository<Connector> { }
	public interface IConnectorTypeData : IListRepository<Connector> { }
	public interface IChannelData : IListRepository<Channel> { }
	public interface IChannelTypeData : IListRepository<TypeRecord> { }
	public interface IHardwareModuleData : IListRepository<HardwareModule> { }
	public interface IHardwareModuleTypeData : IListRepository<TypeRecord> { }

	public interface IHardwareService {
		IHardwareModuleData HardwareModules { get; }
		IHardwareModuleTypeData HardwareModuleTypes { get; }

		//IChannelData Channels { get; }
		//IChannelTypeData ChannelTypes { get; }

		IChannelData Channels { get; }
		IChannelTypeData ChannelTypes { get; }
	}

	public class HardwareService : IHardwareService {
		public IHardwareModuleData HardwareModules { get; }
		public IHardwareModuleTypeData HardwareModuleTypes { get; }
		public IChannelData Channels { get; }
		public IChannelTypeData ChannelTypes { get; }
	}
}