using Leviathan.Alpha.Database;
using Leviathan.Components;
using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Leviathan.SystemProfiles.Postgres;
using Npgsql;
using System;
using System.Threading.Tasks;

[assembly: LeviathanPlugin]
namespace Leviathan.SystemProfiles.Basic {

	[SystemProfile("Basic", "Creates a Postgres SQL database & configures support data for a basic installation, does not configure drivers or connectors.")]
	[RequireProfile(typeof(PostgreSQLProfile))]
	public class BasicProfile : SystemProfileComponent {

		protected ILeviathanSystem System { get; }
		protected IDataSystemService<NpgsqlConnection> Data { get; }

		public BasicProfile(ILeviathanSystem system, IDataSystemService<NpgsqlConnection> data, IComponentsService components) : base(components) {
			this.System = system;
			this.Data = data;
		}

		public override async Task Apply() {
			await System.Initialize;
			await Data.Initialize;
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

				//await c.CreateCommand(Queries.INIT)
				//	.ExecuteNonQueryAsync();
			});
		}
	}
}