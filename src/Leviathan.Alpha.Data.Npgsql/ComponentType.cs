using Leviathan.DbDataAccess.Npgsql;
using Npgsql;
using System.Collections.Generic;

namespace Leviathan.Alpha.Data.Npgsql {
	public record ComponentTypeRecord : StandardEntity<long> {
		public long CategoryId { get; init; }
		public long AssemblyId { get; init; }
		public string TypeName { get; init; }
	}

	public class ComponentTypeRepo : AlphaDbListRepo<ComponentTypeRecord> {
		public ComponentTypeRepo(IListRepoCommands commands, NpgsqlConnection connection) :
			base(commands, connection) {
		}

		public override long Create(ComponentTypeRecord item) {
			throw new System.NotImplementedException();
		}

		public override void Delete(long id) {
			throw new System.NotImplementedException();
		}

		public override IEnumerable<ComponentTypeRecord> List() {
			throw new System.NotImplementedException();
		}

		public override ComponentTypeRecord Read(long id) {
			throw new System.NotImplementedException();
		}

		public override void Update(ComponentTypeRecord item) {
			throw new System.NotImplementedException();
		}
	}
}