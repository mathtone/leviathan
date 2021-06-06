using Leviathan.DataAccess;
using Leviathan.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Services.Core.Hardware {
	public interface IHardwareService {
		IListRepository<HardwareModuleTypeInfo, int> ModuleTypes { get; }
	}

	public class HardwareService : IHardwareService {

		public IListRepository<HardwareModuleTypeInfo, int> ModuleTypes { get; }
		public IListRepository<HardwareModuleInfo, int> Modules { get; }

		public IListRepository<ChannelTypeInfo, int> ChannelTypes { get; }
		public IListRepository<ChannelInfo, int> Channels { get; }

		public IListRepository<ChannelControllerTypeInfo, int> ChannelControllerTypes { get; }
		public IListRepository<ChannelControllerInfo, int> ChannelControllers { get; }

		public HardwareService(
			IListRepository<HardwareModuleTypeInfo, int> moduleTypes,
			IListRepository<HardwareModuleInfo, int> modules,
			IListRepository<ChannelTypeInfo, int> channelTypes,
			IListRepository<ChannelInfo, int> channels,
			IListRepository<ChannelControllerTypeInfo, int> channelControllerTypes,
			IListRepository<ChannelControllerInfo, int> channelControllers) {

			this.ModuleTypes = moduleTypes;
			this.Modules = modules;
			this.ChannelTypes = channelTypes;
			this.Channels = channels;
			this.ChannelControllerTypes = channelControllerTypes;
			this.ChannelControllers = channelControllers;
		}
	}
}