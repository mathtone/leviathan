using Leviathan.DataAccess;

namespace Leviathan.Hardware {

	public interface IRepositoryCatalog<T> : IRepositoryCatalog<long, T> { }
	public interface IRepositoryCatalog<ID, T> : IRepositoryCatalog<ID, T, T> { }
	public interface IRepositoryCatalog<ID, T, C> : IRepository<T,ID>, ICatalog<C> {}

	public interface IConnectorData : IRepositoryCatalog<Connector> { }

	public interface IConnectorTypeData : IRepositoryCatalog<Connector> { }
	public interface IChannelData : IRepositoryCatalog<Channel> { }
	public interface IChannelTypeData : IRepositoryCatalog<TypeInfo> { }
	public interface IHardwareModuleData : IRepositoryCatalog<HardwareModule> { }
	public interface IHardwareModuleTypeData : IRepositoryCatalog<TypeInfo> { }

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