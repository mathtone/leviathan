namespace Leviathan.Hardware {
	public class Connector {
		public int Id { get; set; }
		public int TypeId { get; set; }
		public int HardwareModuleId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public object ConnectorData { get; set; }
	}

	//public interface IItemData<T, C> : IItemData<T, long, C> { }
	//public interface IItemData<T, ID, C> : IRepository<T, ID>, ICatalog<C> { }
	//public interface IChannelData : IRepository<Channel> { }
	//public interface IChannelTypeData : IRepository<TypeInfo> { }
	//public interface IHardwareModuleData : IRepository<HardwareModule> { }
	//public interface IHardwareModuleTypeData : IRepository<TypeInfo> { }
}