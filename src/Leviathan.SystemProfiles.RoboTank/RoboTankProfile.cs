using Leviathan.Alpha.Data.Npgsql;
using Leviathan.Alpha.Database;
using Leviathan.Components;
using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.Hardware.I2C;
using Leviathan.Hardware.PCA9685;
using Leviathan.Hardware.RPIGPIO;
using Leviathan.SDK;
using Leviathan.SystemProfiles.Basic;
using Leviathan.SystemProfiles.FactoryReset;
using Leviathan.SystemProfiles.Hardcore;
using Leviathan.SystemProfiles.Postgres;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static Leviathan.Components.ComponentCategory;
[assembly: LeviathanPlugin("Robo-Tank Profile")]
namespace Leviathan.SystemProfiles.RoboTank {


	[SystemProfile("Robo-Tank", "Applies the 'Basic' profile & configures the Leviathan for the 'Robo-Tank' controller")]
	[RequireProfile(typeof(BasicProfile))]
	public class RoboTankProfile : SystemProfileComponent {

		ILeviathanAlphaDataContext<NpgsqlConnection> _context;
		ILeviathanAlphaDataContextProvider Provider { get; }
		ILeviathanAlphaDataContext<NpgsqlConnection> Context => _context ??= Provider.CreateContext<NpgsqlConnection>();
		NpgsqlConnection Connection => Context.Connection;

		public RoboTankProfile(IComponentsService components, ILeviathanAlphaDataContextProvider contextProvider) :
			base(components) {
			this.Provider = contextProvider;
		}

		public override async Task Apply() {
			await base.ApplyRequired();

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
				GPIO = CreateModule("RPI GPIO", "RPI GPIO Module", drivers.GPIO),
				I2C = CreateModule("I2C", "RPI I2C Module", drivers.GPIO),
				PWM = CreateModule("PCA9685", "Robo-Tank PWM", drivers.PWM)
			};

			long c1 = 0, c2 = 0;
			var gpinout = new[] { 9, 10, 22, 15, 14, 23, 24, 25, 16, 6, 5, 7, 11, 8, 12, 13 }
				.Select(p => new HardwareConnectorRecord {
					Name = $"GPIO{++c1}",
					Description = $"GPIO Pin {c1}",
					ModuleId = modules.GPIO,
					ComponentTypeId = drivers.GPIO_C,
					ConnectorData = new GpioConnectorData {
						Mode = 1,
						Pin = p
					}
				}
			);

			var gpout = new[] { 17, 27, 19, 20, 26, 21 }.Select(p =>
				new HardwareConnectorRecord {
					Name = $"GPIO{c1++}",
					Description = $"GPIO Pin {c1}",
					ModuleId = modules.GPIO,
					ComponentTypeId = drivers.GPIO_C,
					ConnectorData = new GpioConnectorData {
						Mode = 0,
						Pin = p
					}
				}
			);

			var pwm = new[] { 15, 14, 13, 12, 11, 10, 9, 8, 0, 1, 2, 3, 4, 5, 6, 7 }.Select(p =>
				new HardwareConnectorRecord {
					Name = $"PWM{++c2}",
					Description = $"PWM I/O {c2}",
					ModuleId = modules.PWM,
					ComponentTypeId = drivers.PWM_C,
					ConnectorData = new PwmIOConnectorData {
						PwmChannelId = p
					}
				}
			);

			var connectorIds = gpinout.Concat(gpout).Concat(pwm)
				.Select(Context.HardwareConnector.Create)
				.ToArray();

			int c3 = 0, c4 = 0, c5 = 0, c6 = 0;
			var channels = new {
				AC_PORT = connectorIds[0..16].Select(i =>
					CreateChannel(
						$"AC{++c3:00}",
						$"AC Outlet {c3:00}",
						drivers.GPIO_OO_CH, i, new { Mode = 1 })
					).ToArray(),
				SR_PORT = connectorIds[16..22].Select(i =>
					CreateChannel(
						$"SS{++c4:00}",
						$"Sensor Port {c4:00}",
						drivers.GPIO_S_CH, i, new { Mode = 0 })
					).ToArray(),
				DC_PORT = connectorIds[22..30].Select(i =>
					CreateChannel(
						$"DC{++c5:00}",
						$"DC Port {c5:00}",
						drivers.PWM_CH, i)
					).ToArray(),
				PWM_PORT = connectorIds[30..^0].Select(i =>
					CreateChannel(
						$"PWM{++c6:00}",
						$"PWM/Analog Port {c6:00}",
						drivers.PWM_CH, i)
					).ToArray()
			};
		}

		long Register<T>() =>
			Components.RegisterComponent(typeof(T));

		long CreateModule(string name, string description, long typeId) => Context.HardwareModule.Create(new HardwareModuleRecord {
			Name = name,
			ComponentTypeId = typeId,
			Description = description
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