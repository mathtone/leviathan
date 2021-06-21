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

		public RoboTankProfile(IComponentsService components, ILeviathanAlphaDataContextProvider context) :
			base(components) {
			this.Provider = context;
		}

		public override async Task Apply() {
			await base.ApplyRequired();


			var driverTypes = new[] {
				typeof(GpioDriver),
				typeof(I2CDriver),
				typeof(Pca9685Driver),
				typeof(FactoryResetProfile),
				typeof(PostgreSQLProfile),
				typeof(BasicProfile),
				typeof(HardcoreProfile)
			};
			var ids = driverTypes.Select(t=>Components.RegisterComponent(t)).ToArray();
		}
	}

}