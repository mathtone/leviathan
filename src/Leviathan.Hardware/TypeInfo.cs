using Leviathan.DataAccess;
using System;

namespace Leviathan.Hardware {

	public class TypeInfo {
		//public long Id { get; set; }
		public string TypeName { get; set; }
		public string AssemblyName { get; set; }
		public string AssemblyPath { get; set; }
	}

	//public interface IItemData<T, C> : IItemData<T, long, C> { }
	//public interface IItemData<T, ID, C> : IRepository<T, ID>, ICatalog<C> { }
	//public interface IChannelData : IRepository<Channel> { }
	//public interface IChannelTypeData : IRepository<TypeInfo> { }
	//public interface IHardwareModuleData : IRepository<HardwareModule> { }
	//public interface IHardwareModuleTypeData : IRepository<TypeInfo> { }
}