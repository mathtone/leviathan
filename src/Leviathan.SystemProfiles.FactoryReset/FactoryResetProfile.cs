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
namespace Leviathan.SystemProfiles.FactoryReset {

	[SystemProfile("Factory Reset", "Reset to factory new condition.")]
	public class FactoryResetProfile : SystemProfileComponent {

		protected ILeviathanSystem System { get; }
		protected IDataSystemService<NpgsqlConnection> Data { get; }

		public FactoryResetProfile(ILeviathanSystem system, IDataSystemService<NpgsqlConnection> data, IComponentsService components) : base(components) {
			this.System = system;
			this.Data = data;
		}
		public override async Task Apply() {
			var cat = await Data.CatalogAsync();
			using var cn = Data.SystemDB.Connect();
			cn.Open();
			var located = (bool)await cn.CreateCommand(SQL.DB.Locate)
				.WithInput("@db_name", cat.DatabaseInfo.InstanceDbName)
				.ExecuteScalarAsync();
			if (located) {
				await cn.CreateCommand(SQL.DB.Drop)
				   .WithTemplate("@:db-name", cat.DatabaseInfo.InstanceDbName)
				   .ExecuteNonQueryAsync();
			}
			cn.Close();

		}
		//static readonly string DROP = ResourceLoader.LoadLocal("Profiles.Queries.Drop_DB.sqlx");
	}
}