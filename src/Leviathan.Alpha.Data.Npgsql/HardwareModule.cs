using Leviathan.DbDataAccess.Npgsql;
using Npgsql;

namespace Leviathan.Alpha.Data.Npgsql {
	public record HardwareModuleRecord : StandardEntity<long> {
		public long ComponentTypeId { get; init; }
	}

	public class HardwareModuleRepo : AlphaDbDataRepo<HardwareModuleRecord> {
		public HardwareModuleRepo(IListRepoCommands commands, NpgsqlConnection connection) :
			base(commands, connection) {
		}

		public override long Create(HardwareModuleRecord item) {
			throw new System.NotImplementedException();
		}

		public override void Delete(long id) {
			throw new System.NotImplementedException();
		}

		public override HardwareModuleRecord Read(long id) {
			throw new System.NotImplementedException();
		}

		public override void Update(HardwareModuleRecord item) {
			throw new System.NotImplementedException();
		}
	}
}