using Leviathan.DbDataAccess.Npgsql;
using Npgsql;
using System.Collections.Generic;

namespace Leviathan.Alpha.Data.Npgsql {
	public record ComponentCategoryRecord : StandardEntity<long> {

	}

	public class ComponentCategoryRepo : AlphaDbListRepo<ComponentCategoryRecord> {
		
		public ComponentCategoryRepo(IListRepoCommands commands,NpgsqlConnection connection) :
			base(commands,connection) {
		}

		public override long Create(ComponentCategoryRecord item) {
			throw new System.NotImplementedException();
		}

		public override void Delete(long id) {
			throw new System.NotImplementedException();
		}

		public override IEnumerable<ComponentCategoryRecord> List() {
			throw new System.NotImplementedException();
		}

		public override ComponentCategoryRecord Read(long id) {
			throw new System.NotImplementedException();
		}

		public override void Update(ComponentCategoryRecord item) {
			throw new System.NotImplementedException();
		}
	}
}