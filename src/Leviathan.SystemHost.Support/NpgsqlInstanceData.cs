using Leviathan.Data;
using Npgsql;
using Leviathan.Instances;
using Leviathan.Data.Npgsql;
using System.Data;

namespace Leviathan.SystemHost.Support {
	public class NpgsqlInstanceData : NpgsqlRepository<int, InstanceRecord>, IInstanceHostData {
		public NpgsqlInstanceData(IConnectionProvider<NpgsqlConnection> connections) :
			base(connections) { }

		protected override NpgsqlCommand CreateCommand(NpgsqlConnection cn, InstanceRecord item) {
			throw new NotImplementedException();
		}

		protected override NpgsqlCommand DeleteCommand(NpgsqlConnection cn, int id) {
			throw new NotImplementedException();
		}

		protected override InstanceRecord CreateItem(IDataRecord r) {
			throw new NotImplementedException();
		}

		protected override NpgsqlCommand ReadAllCommand(NpgsqlConnection cn) {
			throw new NotImplementedException();
		}

		protected override NpgsqlCommand ReadCommand(NpgsqlConnection cn, int id) {
			throw new NotImplementedException();
		}

		protected override NpgsqlCommand UpdateCommand(NpgsqlConnection cn, InstanceRecord item) {
			throw new NotImplementedException();
		}
	}
}