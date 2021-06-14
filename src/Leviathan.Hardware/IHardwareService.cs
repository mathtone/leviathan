using Leviathan.DataAccess;
using Leviathan.SDK;

namespace Leviathan.Hardware {

	public interface IConnectorData : IListRepository<Connector> { }
	public interface IConnectorTypeData : IListRepository<Connector> { }
	public interface IHardwareModuleData : IListRepository<HardwareModule> { }
	public interface ILeviathanDriverData : IListRepository<TypeRecord> { }

	public interface IHardwareService {
		IHardwareModuleData HardwareModules { get; }
		ILeviathanDriverData DriverTypes { get; }
		IConnectorData Connectors { get; }
		IConnectorTypeData ConnectorTypes { get; }
	}

	public class HardwareService : IHardwareService {
		public IHardwareModuleData HardwareModules { get; }
		public ILeviathanDriverData DriverTypes { get; }
		public IConnectorData Connectors { get; }
		public IConnectorTypeData ConnectorTypes { get; }

		public HardwareService(IHardwareModuleData modules, ILeviathanDriverData driverTypes, IConnectorData connectors, IConnectorTypeData connectorTypes) {
			this.HardwareModules = modules;
			this.DriverTypes = driverTypes;
			this.Connectors = connectors;
			this.ConnectorTypes = connectorTypes;
		}
	}
}