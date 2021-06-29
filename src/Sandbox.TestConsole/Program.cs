using Leviathan.Alpha.Data.Npgsql;
using Leviathan.Alpha.Database;
using Leviathan.Components;
using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.Hardware;
using Leviathan.Services;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sandbox.TestConsole {
	class Program {
		static async Task<int> Main(string[] args) {

			var connections = new InstanceConnectService(() => new NpgsqlConnection(DbConnectionString("poseidonalpha.local", "pi", "Digital!2021", "leviathan_alpha_0x00")));
			var Provider = new LeviathanAlphaDataContextProvider(connections);
			var ctx = Provider.CreateContext<NpgsqlConnection>();

			await ctx.Connection.OpenAsync();

			var Drivers = new Drivers(ctx);
			var Modules = new Modules(ctx, Drivers);
			var Connectors = new Connectors(ctx, Modules);
			var Channels = new Channels(ctx, Connectors);

			await ctx.Connection.CloseAsync();

			for (var i = 1; i <= 16; i++) {
				(Channels[i] as IOutputChannel<bool>).SetValue(true);
			}
			await Task.Delay(1000);
			for (var i = 1; i <= 16; i++) {
				(Channels[i] as IOutputChannel<bool>).SetValue(false);
			}
			 
			return 0;
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

	public class HardwareModule {
		object _device;

		public long Id { get; init; }
		public IDeviceDriver Driver { get; init; }
		public object ModuleData { get; init; }
		public Type ModuleDataType { get; init; }
		public Type DeviceType => Device.GetType();
		public object Device => _device ??= CreateDevice();

		object CreateDevice() {
			var device = Driver.CreateDevice(ModuleData);
			return device;
		}
	}


	public class Modules : ServiceComponent {

		protected ILeviathanAlphaDataContext<NpgsqlConnection> Context { get; }
		Drivers Drivers { get; }
		IDictionary<long, HardwareModule> _modules;

		public HardwareModule this[long id] => _modules[id];

		public Modules(ILeviathanAlphaDataContext<NpgsqlConnection> context, Drivers drivers) {
			this.Context = context;
			this.Drivers = drivers;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {

			await base.Initialize;
			await Drivers.Initialize;

			_modules = (await Context.Connection.CreateCommand(SQL.GET_MODULES).ExecuteReaderAsync().ToArrayAsync(
				r => new {
					Id = r.Get<long>("module_id"),
					Name = r.Get<string>("module_name"),
					ModuleData = r.Get<string>("module_data"),
					TypeLocator = r.Get<string>("type_locator"),
				}
			)).ToDictionary(
				i => i.Id,
				i => CreateModule(i.Id, i.TypeLocator, i.ModuleData)
			);
		}

		HardwareModule CreateModule(long id, string typeLocator, string moduleData) {
			try {
				var type = Type.GetType(typeLocator);
				var i = type.GetInterfaces()
					.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDeviceDriver<,>))
					.Single();

				var argTypes = i.GetGenericArguments();
				var deviceType = argTypes[0];
				var driverDataType = argTypes[1];
				var driverData = JsonConvert.DeserializeObject(moduleData, argTypes[1]);
				var driver = (IDeviceDriver)Activator.CreateInstance(type);

				return new HardwareModule {
					Id = id,
					Driver = driver,
					ModuleData = driverData,
					ModuleDataType = driverDataType
				};
			}
			catch {
				throw;
			}
		}

		class SQL {
			public const string GET_MODULES = @"
			SELECT
				modl.id module_id,
				modl.name module_name,
				modl.module_data module_data,
				modltype.type_locator type_locator
			FROM sys.hardware_module modl
			JOIN sys.component_type modltype ON modl.component_type_id = modltype.id";
		};
	}

	public class Connectors : ServiceComponent {
		protected ILeviathanAlphaDataContext<NpgsqlConnection> Context { get; }
		protected Modules Modules { get; }
		IDictionary<long, object> _connectors;

		public object this[long id] => _connectors[id];
		public Connectors(ILeviathanAlphaDataContext<NpgsqlConnection> context, Modules modeules) {
			this.Context = context;
			this.Modules = modeules;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {
			await base.InitializeAsync();
			await Modules.Initialize;

			_connectors = (await Context.Connection.CreateCommand(SQL.GET_CONNECTORS).ExecuteReaderAsync().ToArrayAsync(
				r => new {
					Id = r.Get<long>("connector_id"),
					ModuleId = r.Get<long>("module_id"),
					Name = r.Get<string>("connector_name"),
					connectorData = r.Get<string>("connector_data"),
					TypeLocator = r.Get<string>("type_locator"),
				}
			)).ToDictionary(
				i => i.Id,
				i => CreateConnector(i.Id, i.ModuleId, i.TypeLocator, i.connectorData)
			);
		}

		object CreateConnector(long id, long moduleId, string typeLocator, string connectorData) {
			try {

				var mod = this.Modules[moduleId];
				var type = Type.GetType(typeLocator);

				foreach (var c in type.GetConstructors()) {
					var p = c.GetParameters().ToArray();
					if (p.Length == 2) {
						if (p[0].ParameterType == mod.DeviceType) {
							var dataType = p[1].ParameterType;
							var rtn = Activator.CreateInstance(type, mod.Device, JsonConvert.DeserializeObject(connectorData, dataType));
							return rtn;
						}
					}
				}

				throw new Exception($"Could not create connector {id}");
			}
			catch {
				throw;
			}
		}

		class SQL {
			public const string GET_CONNECTORS =
			@"
			SELECT
				cnct.id connector_id,
				cnct.module_id,
				cnct.name connector_name,
				cnct.connector_data connector_data,
				cncttype.type_locator type_locator
			FROM sys.hardware_connector cnct
			JOIN sys.component_type cncttype ON cnct.component_type_id = cncttype.id
			";
		};
	}

	public class Channels : ServiceComponent {

		IDictionary<long, IChannel> _channels;

		protected ILeviathanAlphaDataContext<NpgsqlConnection> Context { get; }
		protected Connectors Connectors { get; }
		public IChannel this[long id] => _channels[id];

		public Channels(ILeviathanAlphaDataContext<NpgsqlConnection> context, Connectors connectors) {
			this.Context = context;
			this.Connectors = connectors;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {
			await base.InitializeAsync();
			await Connectors.Initialize;

			_channels = (await Context.Connection.CreateCommand(SQL.GET_CHANNELS).ExecuteReaderAsync().ToArrayAsync(
				r => new {
					Id = r.Get<long>("channel_id"),
					ConnectorId = r.Get<long>("connector_id"),
					Name = r.Get<string>("channel_name"),
					ChannelData = r.Get<string>("channel_data"),
					TypeLocator = r.Get<string>("type_locator"),
				}
			)).ToDictionary(
				i => i.Id,
				i => CreateChannel(i.Id, i.ConnectorId, i.TypeLocator, i.ChannelData)
			);
		}

		IChannel CreateChannel(long id, long connectorId, string typeLocator, string channelData) {
			var con = this.Connectors[connectorId];
			var type = Type.GetType(typeLocator);

			foreach (var c in type.GetConstructors()) {
				var p = c.GetParameters().ToArray();
				if (p.Length == 2) {

					;
					//;
					if (p[0].ParameterType == con.GetType()) {
						var dataType = p[1].ParameterType;
						var rtn = (IChannel)Activator.CreateInstance(type, con, JsonConvert.DeserializeObject(channelData, dataType));
						return rtn;
					}
				}
			}
			throw new Exception($"Could not create channel {id}");
		}

		class SQL {
			public const string GET_CHANNELS =
			@"
			SELECT
				chnl.id channel_id,
				chnl.id connector_id,
				chnl.name channel_name,
				chnl.channel_data channel_data,
				chnltype.type_locator type_locator
			FROM sys.hardware_channel chnl
			JOIN sys.component_type chnltype ON chnl.component_type_id = chnltype.id;
			";
		};
	}
}