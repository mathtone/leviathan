using Leviathan.DbDataAccess.Npgsql;
using Npgsql;
using System.Collections.Generic;

namespace Leviathan.Alpha.Data.Npgsql {
	public record ComponentAssemblyRecord : StandardEntity<long> {
		public string AssemblyName { get; init; }
		public string AssemblyPath { get; init; }
	}

	public class ComponentAssemblyRepo : AlphaDbListRepo<ComponentAssemblyRecord> {
		public ComponentAssemblyRepo(IListRepoCommands commands, NpgsqlConnection connection) : base(commands,connection) {
		}

		public override long Create(ComponentAssemblyRecord item) {
			throw new System.NotImplementedException();
		}

		public override void Delete(long id) {
			throw new System.NotImplementedException();
		}

		public override IEnumerable<ComponentAssemblyRecord> List() {
			throw new System.NotImplementedException();
		}

		public override ComponentAssemblyRecord Read(long id) {
			throw new System.NotImplementedException();
		}

		public override void Update(ComponentAssemblyRecord item) {
			throw new System.NotImplementedException();
		}
	}
}