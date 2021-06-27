using Leviathan.Alpha.Data.Npgsql;
using Leviathan.Alpha.Database;
using Leviathan.Components;
using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Leviathan.Services;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Hardware {
	public interface ILeviathanHardwareSystemService : IAsyncInitialize {
		Task<object> Test();
	}

	public record HardwareModule : HardwareModuleRecord {
		public Type Type { get; init; }
		public IDeviceDriver Instance { get; init; }
	}

	public class Drivers {
		
	}

	public class Modules {
		
	}

	public class Connectors {

	}

	public class Channels {

	}

	public class LeviathanHardwareSystemService : ServiceComponent, ILeviathanHardwareSystemService {

		readonly ILeviathanAlphaDataContextProvider _provider;
		ILeviathanAlphaDataContext<NpgsqlConnection> _context;

		ILeviathanAlphaDataContext<NpgsqlConnection> Context => _context ??= _provider.CreateContext<NpgsqlConnection>();

		public LeviathanHardwareSystemService(ILeviathanAlphaDataContextProvider provider) {
			this._provider = provider;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {
			await base.InitializeAsync();

		}

		public async Task<object> Test() {
			await Initialize;
			try {
				await Context.Connection.OpenAsync();
				var modules = await CreateModules().ToDictionaryAsync(m => m.Id);
				await Context.Connection.CloseAsync();
				return null;
			}
			catch (Exception ex) {
				throw new Exception(ex.Message);
			}

			//Load Modules 
			//Load Connectors
			//Load Types
			//throw new NotImplementedException();
		}

		async IAsyncEnumerable<HardwareModule> CreateModules() {
			var modules = (await Context.Connection
				.CreateCommand(LIST_MODULES)
				.ExecuteReaderAsync())
				.ToArray(r => new {
					Id = r.Field<long>("id"),
					Name = r.Field<string>("name"),
					Description = r.Field<string>("description"),
					ModuleData = r.Field<string>("module_data"),
					Type = Type.GetType(r.Field<string>("type_locator")),
				});
			foreach (var m in modules) {
				yield return new HardwareModule {
					Id = m.Id,
					Name = m.Name,
					Description = m.Description,
					Type = m.Type,
					ModuleData = m.ModuleData,
					Instance = CreateDriver(m.Type, m.ModuleData)
				};
			}
		}

		static IDeviceDriver CreateDriver(Type type, string moduleData) {

			var i = type.GetInterfaces()
				.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDeviceDriver<,>))
				.Single();

			var argTypes = i.GetGenericArguments();
			var deviceType = argTypes[0];
			var driverDataType = argTypes[1];
			var driverData = JsonConvert.DeserializeObject(moduleData, argTypes[1]);
			return (IDeviceDriver)Activator.CreateInstance(type);
		}

		const string LIST_MODULES =
		@"
			SELECT * FROM sys.hardware_module mdl
			JOIN sys.component_type typ on mdl.component_type_id = typ.id
		";
	}


	////public class HardwareModule {
	////}

	//public class HardwareData {
	//	public IDictionary<long, ComponentCategoryRecord> Categories { get; init; }
	//	public IDictionary<long, ComponentAssemblyRecord> Assemblies { get; init; }
	//	public IDictionary<long, ComponentTypeRecord> Types { get; init; }
	//	public IDictionary<long, HardwareModuleRecord> Modules { get; init; }

	//	//public IDictionary<long, Type> Channels { get; init; }
	//	//public IDictionary<long, Type> Connectors { get; init; }

	//	//public IDictionary<long, ComponentCategoryRecord> Categories { get; init; }
	//	//public IDictionary<long, ComponentAssemblyRecord> Assemblies { get; init; }
	//	//public IDictionary<long, ComponentTypeRecord> Types { get; init; }
	//	//public IDictionary<long, HardwareModuleRecord> Modules { get; init; }
	//	//public IDictionary<long, HardwareChannelRecord> Channels { get; init; }
	//	//public IDictionary<long, HardwareConnectorRecord> Connectors { get; init; }		
	//}
}