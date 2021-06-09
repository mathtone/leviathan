using System.Data;

namespace Leviathan.DataAccess {
	public interface IDbConnectionProvider<CN> where CN : IDbConnection {
		CN Connect();
	}
}