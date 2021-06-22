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

			var driverTypes = new[] {
				typeof(LeviathanGpio),
				typeof(LeviathanI2C),
				typeof(LeviathanPca9685),
				typeof(GpioConnector),
				typeof(PwmIOConnector),
				typeof(FactoryResetProfile),
				typeof(PostgreSQLProfile),
				typeof(BasicProfile),
				typeof(HardcoreProfile)
			};

			var ids = driverTypes
				.Select(t => new {
					Id = Components.RegisterComponent(t),
					Type = t
				})
				.ToDictionary(t => t.Type, t => t.Id);

			var gpio = ids[driverTypes[0]];
			var i2c = ids[driverTypes[1]];
			var pwm = ids[driverTypes[2]];

			gpio = Context.HardwareModule.Create(new HardwareModuleRecord {
				Name = "RPI GPIO",
				ComponentTypeId = gpio,
				Description = "RPI GPIO Module"
			});

			i2c = Context.HardwareModule.Create(new HardwareModuleRecord {
				Name = "I2C",
				ComponentTypeId = gpio,
				Description = "RPI I2C Module"
			});

			pwm = Context.HardwareModule.Create(new HardwareModuleRecord {
				Name = "PCA9685",
				ComponentTypeId = pwm,
				Description = "Robo-Tank PWM"
			});



			var i = 1;
			var gpinout = new[] { 9, 10, 22, 15, 14, 23, 24, 25, 16, 6, 5, 7, 11, 8, 12, 13 }.Select(p =>
				new HardwareConnectorRecord {
					Name = $"GPIO{i++}",
					Description = $"GPIO Pin {i++}",
					ModuleId = gpio,
					ComponentTypeId = ids[driverTypes[3]],
					ConnectorData = new GpioConnectorData {
						Mode = 1,
						Pin = p
					}
				}
			);

			var gpout = new[] { 17, 27, 19, 20, 26, 21 }.Select(p =>
				new HardwareConnectorRecord {
					Name = $"GPIO{i++}",
					Description = $"GPIO Pin {i++}",
					ModuleId = gpio,
					ComponentTypeId = ids[driverTypes[3]],
					ConnectorData = new GpioConnectorData {
						Mode = 0,
						Pin = p
					}
				}
			);

			var pinIds = gpinout.Select(Context.HardwareConnector.Create).Concat(
					gpout.Select(Context.HardwareConnector.Create)
				).ToArray();
			;

			/*
			SELECT
				conn.id,
				conn.name connector_name,
				conn.connector_data,
				conntype.type_name type_name,
				conncategory.name category
			FROM sys.hardware_connector conn
			JOIN sys.component_type conntype ON conn.component_type_id = conntype.id 
			JOIN sys.component_category conncategory ON conntype.component_category_id = conncategory.id 
			*/
		}
	}

	public class GpioConnectorData {

		public int Pin { get; init; }
		public int Mode { get; init; }
	}

	[LeviathanConnector("GPIO PIN", "Gpio Connector")]
	public class GpioConnector {
		public GpioConnector(GpioController device, GpioConnectorData connectorData) {
			;
		}
	}


	public class PwmIOConnectorData {

		public int Pin { get; }
		public int Mode { get; set; }
	}

	[LeviathanConnector("PWM IO", "PCA9685 PWM Connector")]
	public class PwmIOConnector {
		public PwmIOConnector(LeviathanPca9685 device, PwmIOConnectorData connectorData) {
			;
		}
	}

	//public class GpioChannelConfig : ChannelConfig {
	//	public int Pin { get; set; }
	//	public PinMode Mode { get; set; }
	//}

	//public class GpioChannel : InputOutputChannel<PinValue, GpioModule, GpioChannelConfig> {

	//	public int Pin => Config.Pin;
	//	public PinMode Mode => Config.Mode;

	//	public override PinValue Value {
	//		get => Device.Controller.Read(Pin);
	//		set => Device.Controller.Write(Pin, value);
	//	}

	//	public GpioChannel(GpioModule device, GpioChannelConfig config) : base(device, config) {
	//		this.Device.Controller.OpenPin(Pin, Mode);
	//	}
	//}


	//var gpioConnectors = new[] { 9, 10, 22, 15, 14, 23, 24, 25, 16, 6, 5, 7, 11, 8, 12, 13 }.Select(p =>
	//	new HardwareConnectorRecord {
	//		Name = $"GPIO{i++}",
	//		Description = $"GPIO Pin {i++}",
	//		ModuleId = gpio
	//		//ComponentTypeId = (gpio connector)
	//	}
	//);

	//new ChannelInfo(
	//	modules[0].Id,
	//	ChannelTypes[0].Id,
	//	$"GPIO {i++}",
	//	SerializeObject(new { Pin = p, Mode = 1 })
	//)
}