using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;

namespace Leviathan.Alpha.Data {
	public class AlphaDbDataRepo<T> : AlphaDbDataRepo<long, T> { }
	public class AlphaDbDataRepo<ID, T> : NpgSqlDataRepository<ID, T> { }
}