using Leviathan.Alpha.Database;
using Leviathan.Components;
using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Npgsql;
using System;
using System.Threading.Tasks;

[assembly: LeviathanPlugin]
namespace Leviathan.SystemProfiles.Postgres {

	[SystemProfile("PostgreSQL", "Creates a database & data environment for using a PostgreSQL database,")]
	public class PostgreSQLProfile : SystemProfileComponent {
		IDataSystemService<NpgsqlConnection> Data { get; }
		public PostgreSQLProfile(IComponentsService components, IDataSystemService<NpgsqlConnection> data) : base(components) {
			this.Data = data;
		}

		public async override Task Apply() {
			await base.ApplyRequired();

			var cat = await Data.CatalogAsync();
			await Data.ConnectSystem().UsedAsync(async c => {

				var located = (bool)await c.CreateCommand(SQL.DB.Locate)
					.WithInput("@db_name", cat.DatabaseInfo.InstanceDbName)
					.ExecuteScalarAsync();

				if (located) {
					throw new Exception("Database already located, factory reset required.");
				}

				await c.CreateCommand(SQL.DB.Create)
					.WithTemplate("@:db_name", cat.DatabaseInfo.InstanceDbName)
					.WithTemplate("@:owner", cat.DbOwner)
					.ExecuteNonQueryAsync();

			});

			await Data.ConnectInstance().UsedAsync(async c => {
				await c.CreateCommand(SQL.DB.Init).ExecuteNonQueryAsync();
				
				var cmd = c.CreateCommand(@"
					INSERT INTO sys.component_category(name,description)
					VALUES(@name,@description)
					RETURNING id;"
				);

				await cmd.WithInput("@name", "System Profile")
					.WithInput("@description", "System configuration profiles")
					.ExecuteNonQueryAsync();

				await cmd.WithInput("@name", "Device Driver")
					.WithInput("@description", "Hardware device driver/module")
					.ExecuteNonQueryAsync();

				await cmd.WithInput("@name", "Service")
					.WithInput("@description", "Injectible service component.")
					.ExecuteNonQueryAsync();

			});
		}
	}
}