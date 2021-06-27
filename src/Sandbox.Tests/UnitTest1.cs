using Leviathan.Alpha.Data.Npgsql;
using Leviathan.Alpha.Database;
using Leviathan.Alpha.Hardware;
using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Leviathan.Services;
using Npgsql;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Leviathan.Components;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sandbox.Tests {
	public class UnitTest1 {

		ILeviathanAlphaDataContextProvider Provider { get; }

		public UnitTest1() {
			var connections = new InstanceConnectService(() => new NpgsqlConnection(DbConnectionString("poseidonalpha.local", "pi", "Digital!2021", "leviathan_alpha_0x00")));
			Provider = new LeviathanAlphaDataContextProvider(connections);
		}

		[Fact]
		public async Task HardwareSystemTests() {
			var ctx = Provider.CreateContext<NpgsqlConnection>();
			await ctx.Connection.OpenAsync();

			var Drivers = new Drivers(ctx);
			var Modules = new Modules(ctx, Drivers);
			var Connectors = new Connectors(ctx, Modules);
			var Channels = new Channels(ctx, Connectors);
			;
			await ctx.Connection.CloseAsync();

			var devices = Modules[3];
		}

		static string DbConnectionString(string hostName, string login, string password, string database) =>
			$"Host={hostName};Username={login};Database={database};Password={password};";

		
	}

	

	public class Drivers : ServiceComponent {

		protected ILeviathanAlphaDataContext<NpgsqlConnection> Context { get; }

		public Drivers(ILeviathanAlphaDataContext<NpgsqlConnection> context) {
			this.Context = context;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {
			await base.InitializeAsync();
		}
	}

	public class Modules : ServiceComponent {
		IDictionary<long, IDeviceDriver> _drivers;

		protected ILeviathanAlphaDataContext<NpgsqlConnection> Context { get; }
		Drivers Drivers { get; }

		public IDeviceDriver this[long id] => _drivers[id];

		public Modules(ILeviathanAlphaDataContext<NpgsqlConnection> context, Drivers drivers) {
			this.Context = context;
			this.Drivers = drivers;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {

			await base.InitializeAsync();
			await Drivers.Initialize;

			_drivers = (await Context.Connection.CreateCommand(SQL.GET_MODULES).ExecuteReaderAsync().ToArrayAsync(
				r => new {
					Id = r.Field<long>("module_id"),
					Name = r.Field<string>("module_name"),
					ModuleData = r.Field<string>("module_data"),
					TypeLocator = r.Field<string>("type_locator"),
				}
			)).ToDictionary(
				i => i.Id,
				i => CreateDriver(i.Id, i.TypeLocator, i.ModuleData)
			);

			//modules
		}
		static IDeviceDriver CreateDriver(long id, string typeLocator, string moduleData) {
			var type = Type.GetType(typeLocator);
			var i = type.GetInterfaces()
				.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDeviceDriver<,>))
				.Single();

			var argTypes = i.GetGenericArguments();
			var deviceType = argTypes[0];
			var driverDataType = argTypes[1];
			var driverData = JsonConvert.DeserializeObject(moduleData, argTypes[1]);
			var driver = (IDeviceDriver)Activator.CreateInstance(type);
			var device = driver.CreateDevice(driverData);
			;
			return driver;
		}
		class SQL {
			public const string GET_MODULES =
			@"
			SELECT
				modl.id module_id,
				modl.name module_name,
				modl.module_data module_data,
				modltype.type_locator type_locator
			FROM sys.hardware_module modl
			JOIN sys.component_type modltype ON modl.component_type_id = modltype.id
			";
		};
	}

	public class Connectors : ServiceComponent {
		protected ILeviathanAlphaDataContext<NpgsqlConnection> Context { get; }
		protected Modules Modules { get; }

		public Connectors(ILeviathanAlphaDataContext<NpgsqlConnection> context, Modules modeules) {
			this.Context = context;
			this.Modules = modeules;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {
			await base.InitializeAsync();
			await Modules.Initialize;
		}

		class SQL {
			public const string GET_CONNECTORS =
			@"
			SELECT
				modl.id module_id,
				modl.name module_name,
				modl.module_data module_data,
				modltype.type_locator type_locator
			FROM sys.hardware_module modl
			JOIN sys.component_type modltype ON modl.component_type_id = modltype.id
			";
		};
	}

	public class Channels : ServiceComponent {

		protected ILeviathanAlphaDataContext<NpgsqlConnection> Context { get; }
		protected Connectors Connectors { get; }

		public Channels(ILeviathanAlphaDataContext<NpgsqlConnection> context, Connectors connectors) {
			this.Context = context;
			this.Connectors = connectors;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {
			await base.InitializeAsync();
			await Connectors.Initialize;

		}
	}
}