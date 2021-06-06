using Leviathan.DataAccess;
using Leviathan.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Services.Core.Hardware {
	public interface IHardwareService {
		IHardwareModuleData Modules { get; }
		IHardwareModuleTypeData ModuleTypes { get; }
		IChannelTypeData ChannelTypes { get; }
		IChannelData Channels { get; }
		IChannelControllerTypeData ChannelControllerTypes { get; }
		IChannelControllerData ChannelControllers { get; }
	}

	public interface IHardwareModuleData : IListRepository<HardwareModuleInfo, int> { }
	public interface IHardwareModuleTypeData : IListRepository<HardwareModuleTypeInfo, int> { }
	public interface IChannelTypeData : IListRepository<ChannelTypeInfo, int> { }
	public interface IChannelData : IListRepository<ChannelInfo, int> { }
	public interface IChannelControllerTypeData : IListRepository<ChannelControllerTypeInfo, int> { }
	public interface IChannelControllerData : IListRepository<ChannelControllerInfo, int> {
		IEnumerable<ChannelControllerCatalogItem> Catalog();
	}

	public class HardwareService : IHardwareService {

		public IHardwareModuleTypeData ModuleTypes { get; }
		public IHardwareModuleData Modules { get; }
		public IChannelTypeData ChannelTypes { get; }
		public IChannelData Channels { get; }
		public IChannelControllerTypeData ChannelControllerTypes { get; }
		public IChannelControllerData ChannelControllers { get; }

		public HardwareService(IHardwareModuleTypeData moduleTypes, IHardwareModuleData modules, IChannelTypeData channelTypes,
			IChannelData channels, IChannelControllerTypeData channelControllerTypes, IChannelControllerData channelControllers) {

			this.ModuleTypes = moduleTypes;
			this.Modules = modules;
			this.ChannelTypes = channelTypes;
			this.Channels = channels;
			this.ChannelControllerTypes = channelControllerTypes;
			this.ChannelControllers = channelControllers;
		}
	}
}