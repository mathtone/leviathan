using Npgsql;
using System.Data;
using System.Threading.Tasks;

namespace Leviathan.DataAccess.Npgsql {

	public class DbServerInfo {
		public string HostName { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
	}

	public class NpgsqlConnectionProvider : IDbConnectionProvider<NpgsqlConnection,string> {

		//DbServerInfo serverInfo;
		public Task Initialization => throw new System.NotImplementedException();

		public NpgsqlConnectionProvider() {
			//.this.serverInfo = serverInfo;
		}

		public NpgsqlConnection Get(string connectionString) =>
			new NpgsqlConnection(connectionString);

		//public void SetConnectionInfo(string connectionInfo) {
		//	this.ConnectionString = connectionInfo;
		//}

		//IDbConnection IDbConnectionProvider<IDbConnection>.Connect() => Connect();
	}
}