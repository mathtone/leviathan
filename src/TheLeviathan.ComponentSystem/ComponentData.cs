using Leviathan.DataAccess;
using Leviathan.Npgsql.Sdk;
using Leviathan.Services;
using Npgsql;
using System.Data;
using System.Threading.Tasks;

namespace TheLeviathan.ComponentSystem {
	public interface IComponentData {
		IAsyncRepository<int, AssemblyRecord> Assembly { get; }
	}

	//[TransientService(typeof(IComponentData))]
	//public class ComponentData : IComponentData {
	//	public ComponentData() { }
	//}


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

	public record AssemblyRecord {
		public int Id { get; init; }
		public string Path { get; init; }
		public string Description { get; set; }
	}

	//public record TypeRecord {
	//	public int Id { get; init; }
	//	public int AssemblyId { get; set; }
	//	public string Name { get; set; }
	//	public string Description { get; set; }
	//}

	//public record ComponentRecord {
	//	public int Id { get; init; }
	//	public int TypeId { get; set; }
	//	public string Name { get; set; }
	//	public string Description { get; set; }
	//}

	//public record ComponentCategory {
	//	public int Id { get; init; }
	//	public string Name { get; set; }
	//	public string Description { get; set; }
	//}
}