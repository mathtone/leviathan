using Leviathan.Common;
using System;
using System.Threading.Tasks;
using TheLeviathan.ComponentSystem.Data;

namespace TheLeviathan.ComponentSystem.FileData {
	public class AssemblyRepo : IAsyncRepository<int, AssemblyRecord> {
		public AssemblyRepo() { }

		public Task<int> CreateAsync(AssemblyRecord item) {
			throw new System.NotImplementedException();
		}

		public Task<AssemblyRecord> ReadAsync(int id) {
			throw new System.NotImplementedException();
		}

		public Task UpdateAsync(AssemblyRecord item) {
			throw new System.NotImplementedException();
		}

		public Task DeleteAsync(int id) {
			throw new System.NotImplementedException();
		}
	}
}
