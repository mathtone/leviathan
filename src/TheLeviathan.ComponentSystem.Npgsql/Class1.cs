using Leviathan.Components.Sdk;
using Leviathan.Npgsql.Sdk;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace TheLeviathan.ComponentSystem.Npgsql {
	public class AssemblyRepo : NpgsqlRepository<int, AssemblyRecord> {
		public AssemblyRepo(NpgsqlConnection connection) :
			base(connection) {
		}

		public override Task<int> CreateAsync(AssemblyRecord item) {
			throw new System.NotImplementedException();
		}

		public override Task<AssemblyRecord> ReadAsync(int id) {
			throw new System.NotImplementedException();
		}

		public override Task UpdateAsync(AssemblyRecord item) {
			throw new System.NotImplementedException();
		}

		public override Task DeleteAsync(int id) {
			throw new System.NotImplementedException();
		}
	}
}