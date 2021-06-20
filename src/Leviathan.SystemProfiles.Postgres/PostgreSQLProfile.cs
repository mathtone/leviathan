using Leviathan.Alpha.Database;
using Leviathan.Components;
using Leviathan.SDK;
using Npgsql;
using System;
using System.Threading.Tasks;

[assembly: LeviathanPlugin]
namespace Leviathan.SystemProfiles.Postgres {

	[SystemProfile("PostgreSQL", "Creates a database & data environment for using a PostgreSQL database,")]
	public class PostgreSQLProfile : SystemProfileComponent {
		public PostgreSQLProfile(IComponentsService components) : base(components) {
		}

		public async override Task Apply() {
			await base.ApplyRequired(); 
		}
	}
}