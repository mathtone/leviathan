using Leviathan.Alpha.Data.Npgsql;
using Leviathan.Components;
using Leviathan.DbDataAccess;
using Leviathan.Hardware.I2C;
using Leviathan.Hardware.PCA9685;
using Leviathan.Hardware.RPIGPIO;
using Leviathan.SystemProfiles.Basic;
using Leviathan.SystemProfiles.FactoryReset;
using Npgsql;
using System.Linq;
using System.Threading.Tasks;

[assembly: LeviathanPlugin("Robo-Tank Profile")]
namespace Leviathan.SystemProfiles.RoboTank {


	[SystemProfile("Robo-Tank", "Applies the 'Basic' profile & configures the Leviathan for the 'Robo-Tank' controller")]
	[RequireProfile(typeof(BasicProfile))]
	public class RoboTankProfile : SystemProfileComponent {

		ILeviathanAlphaDataContext<NpgsqlConnection> _context;
		ILeviathanAlphaDataContextProvider Provider { get; }
		ILeviathanAlphaDataContext<NpgsqlConnection> Context => _context ??= Provider.CreateContext<NpgsqlConnection>();

		static readonly int[] GPIOPins = new[] { 9, 10, 22, 15, 14, 23, 24, 25, 16, 6, 5, 7, 11, 8, 12, 13, 17, 27, 19, 20, 26, 21 };
		static readonly int[] PWMPins = new[] { 15, 14, 13, 12, 11, 10, 9, 8, 0, 1, 2, 3, 4, 5, 6, 7 };

		public RoboTankProfile(IComponentsService components, ILeviathanAlphaDataContextProvider contextProvider) :
			base(components) {
			this.Provider = contextProvider;
		}

		public override async Task Apply() {
			await base.ApplyRequired();
			long c1 = 0, c2 = 0;

			var drivers = new {
				RESET_PROFILE = Register<FactoryResetProfile>(),
				GPIO = Register<LeviathanGpio>(),
				I2C = Register<LeviathanI2C>(),
				PWM = Register<LeviathanPca9685>(),
				GPIO_C = Register<GpioConnector>(),
				PWM_C = Register<PwmIOConnector>(),
				GPIO_CH = Register<GpioChannel>(),
				GPIO_S_CH = Register<GpioSensorChannel>(),
				GPIO_OO_CH = Register<GpioOnOffChannel>(),
				PWM_CH = Register<PwmChannel>(),
				PWM_S_CH = Register<PwmSensorChannel>(),
				PWM_OO_CH = Register<PwmOnOffChannel>()
			};

			var modules = new {
				GPIO = CreateModule("RPI GPIO", "RPI GPIO Module", drivers.GPIO, null),
				I2C = CreateModule("I2C", "RPI I2C Module", drivers.GPIO, null),
				PWM = CreateModule("PCA9685", "Robo-Tank PWM", drivers.PWM, new { Settings = new { BusId = 1, DeviceAddress = 64 } })
			};

			var connectors = new {
				GPInOut = GPIOPins.Select(p => CreateConnector($"GPIO{++c1}", $"GPIO Pin {c1}", drivers.GPIO_C, modules.GPIO, new { Pin = p })).ToArray(),
				PWM = PWMPins.Select(p => CreateConnector($"PWM{++c2}", $"PWM I/O {c2}", drivers.PWM_C, modules.PWM, new { PwmChannelId = p })).ToArray()
			};

			int c3 = 0, c4 = 0, c5 = 0, c6 = 0;
			var channels = new {
				AC_PORT = connectors.GPInOut[0..16].Select(i =>
					CreateChannel(
						$"AC{++c3:00}",
						$"AC Outlet {c3:00}",
						drivers.GPIO_OO_CH, i, new { Mode = 1 })
					).ToArray(),
				SR_PORT = connectors.GPInOut[16..22].Select(i =>
					CreateChannel(
						$"SS{++c4:00}",
						$"Sensor Port {c4:00}",
						drivers.GPIO_S_CH, i, new { Mode = 0 })
					).ToArray(),
				DC_PORT = connectors.PWM[0..8].Select(i =>
					CreateChannel(
						$"DC{++c5:00}",
						$"DC Port {c5:00}",
						drivers.PWM_CH, i)
					).ToArray(),
				PWM_PORT = connectors.PWM[8..^0].Select(i =>
					CreateChannel(
						$"PWM{++c6:00}",
						$"PWM/Analog Port {c6:00}",
						drivers.PWM_CH, i)
					).ToArray()
			};
		}

		long Register<T>() =>
			Components.RegisterComponent(typeof(T));

		long CreateModule(string name, string description, long typeId, object moduleData) => Context.HardwareModule.Create(new HardwareModuleRecord {
			Name = name,
			ComponentTypeId = typeId,
			Description = description,
			ModuleData = moduleData
		});

		long CreateConnector(string name, string description, long typeId, long moduleId, object connectorData = default) => Context.HardwareConnector.Create(new HardwareConnectorRecord {
			Name = name,
			ComponentTypeId = typeId,
			Description = description,
			ModuleId = moduleId,
			ConnectorData = connectorData ?? new object()

		});

		long CreateChannel(string name, string description, long typeId, long connectorId, object channelData = default) => Context.HardwareChannel.Create(new HardwareChannelRecord {
			Name = name,
			ComponentTypeId = typeId,
			Description = description,
			ConnectorId = connectorId,
			ChannelData = channelData ?? new object()
		});
	}
}

/*
	SELECT
		ch.id channel_id,
		ch.name channel_name,
		conn.id connector_id,
		conn.name connector_name,
		conntype.type_name connector_type_name,
		conn.connector_data,
		chtype.type_name channel_type_name,
		ch.channel_data
	FROM sys.hardware_connector conn
	JOIN sys.component_type conntype ON conn.component_type_id = conntype.id 
	JOIN sys.component_category conncategory ON conntype.component_category_id = conncategory.id 
	JOIN sys.hardware_channel ch ON conn.id = ch.connector_id
	JOIN sys.component_type chtype ON ch.component_type_id = chtype.id 
*/