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

		public HardwareService(IListRepository<HardwareModuleTypeInfo, int> moduleTypes, IListRepository<ChannelTypeInfo, int> channelTypes) {
			this.ModuleTypes = moduleTypes;
			this.ChannelTypes = channelTypes;
		}
	}
}