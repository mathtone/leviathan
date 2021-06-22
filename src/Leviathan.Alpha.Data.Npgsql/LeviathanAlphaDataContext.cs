using Leviathan.Alpha.Database;
using Leviathan.SDK;
using Npgsql;
using System.Data;

namespace Leviathan.Alpha.Data.Npgsql {

	public interface ILeviathanAlphaDataContext<out CN> : ILeviathanAlphaDataContext where CN : IDbConnection {
		CN Connection { get; }

	}
	public interface ILeviathanAlphaDataContext {
		IComponentAssemblyRepo ComponentAssembly { get; }
		IComponentCategoryRepo ComponentCategory { get; }
		IComponentTypeRepo ComponentType { get; }
		IHardwareModuleRepo HardwareModule { get; }
		IHardwareConnectorRepo HardwareConnector { get; }

	}

	public interface ILeviathanAlphaDataContextProvider {
		ILeviathanAlphaDataContext<CN> CreateContext<CN>() where CN : IDbConnection;
		ILeviathanAlphaDataContext CreateContext();
	}

	public class LeviathanAlphaDataContext : ILeviathanAlphaDataContext<NpgsqlConnection> {

		//IDbConnectionService<NpgsqlConnection> _connectionProvider;
		readonly NpgsqlConnection _connection;

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

		public NpgsqlConnection Connection => _connection;

		public LeviathanAlphaDataContext(IDbConnectionService<NpgsqlConnection> connectionProvider) {
			//this._connectionProvider = connectionProvider;
			this._connection = connectionProvider.Connect();
		}
	}


	public class LeviathanAlphaDataContextProvider : ILeviathanAlphaDataContextProvider {
		readonly IInstanceConnectionService<NpgsqlConnection> _provider;

		public ILeviathanAlphaDataContext CreateContext() => CreateContext<IDbConnection>();
		public ILeviathanAlphaDataContext<CN> CreateContext<CN>() where CN : IDbConnection =>
			(ILeviathanAlphaDataContext<CN>)new LeviathanAlphaDataContext(_provider);

		public LeviathanAlphaDataContextProvider(IInstanceConnectionService<NpgsqlConnection> provider) {
			this._provider = provider;
		}
	}
}