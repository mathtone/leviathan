using Leviathan.Alpha.Database;
using Leviathan.SDK;
using Npgsql;

namespace Leviathan.Alpha.Data.Npgsql {
	public interface ILeviathanAlphaDataContext {
		IComponentAssemblyRepo ComponentAssembly { get; }
		IComponentCategoryRepo ComponentCategory { get; }
		IComponentTypeRepo ComponentType { get; }

		IHardwareModuleRepo HardwareModule { get; }
		IHardwareConnectorRepo HardwareConnector { get; }
	}

	public class LeviathanAlphaDataContext : ILeviathanAlphaDataContext {

		IDbConnectionService<NpgsqlConnection> _connectionProvider;
		NpgsqlConnection _connection;

		IComponentAssemblyRepo _componentAssembly;
		IComponentCategoryRepo _componentCategory;
		IComponentTypeRepo _componentType;
		IHardwareModuleRepo _hardwareModule;
		IHardwareConnectorRepo _hardwareConnector;

		public IComponentAssemblyRepo ComponentAssembly => _componentAssembly ??= new ComponentAssemblyRepo(_connection);
		public IComponentCategoryRepo ComponentCategory => _componentCategory ??= new ComponentCategoryRepo(_connection);
		public IComponentTypeRepo ComponentType => _componentType ??= new ComponentTypeRepo(_connection);

		public IHardwareModuleRepo HardwareModule => _hardwareModule ??= new HardwareModuleRepo(_connection);
		public IHardwareConnectorRepo HardwareConnector => _hardwareConnector ??= new HardwareConnectorRepo(_connection);


		public LeviathanAlphaDataContext(IDbConnectionService<NpgsqlConnection> connectionProvider) {
			this._connectionProvider = connectionProvider;
			this._connection = connectionProvider.Connect();
		}
	}

	public interface ILeviathanAlphaDataContextProvider {
		ILeviathanAlphaDataContext CreateContext();
	}

	public class LeviathanAlphaDataContextProvider : ILeviathanAlphaDataContextProvider {
		IInstanceConnectionService<NpgsqlConnection> _provider;
		public ILeviathanAlphaDataContext CreateContext() => new LeviathanAlphaDataContext(_provider);

		public LeviathanAlphaDataContextProvider(IInstanceConnectionService<NpgsqlConnection> provider) {
			this._provider = provider;
		}
	}
}