using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Npgsql;
using System.Collections.Generic;

namespace Leviathan.Alpha.Data {
	//public abstract class AlphaDbDataRepo<T> : AlphaDbDataRepo<T, IRepoCommands> {
	//	protected AlphaDbDataRepo(IRepoCommands commands, NpgsqlConnection connection) : base(commands,connection) {
	//	}
	//}
	public abstract class AlphaDbDataRepo<T> : AlphaDbDataRepo<long, T> {
		protected AlphaDbDataRepo(NpgsqlConnection connection) :
			base(connection) { }
	}

	public abstract class AlphaDbDataRepo<ID, T> : NpgSqlDataRepository<ID, T> {
		protected AlphaDbDataRepo(NpgsqlConnection connection) :
			base(connection) { }
	}

	public abstract class AlphaDbListRepo<T> : AlphaDbListRepo<long, T> {
		protected AlphaDbListRepo(NpgsqlConnection connection) : base(connection) { }
	}


	//public abstract class AlphaDbListRepo<ID, T> : AlphaDbListRepo<ID, T> {
	//	protected AlphaDbListRepo(NpgsqlConnection connection) : base(connection) { }
	//}

	public abstract class AlphaDbListRepo<ID, T> : AlphaDbDataRepo<ID, T>, IListRepository<ID, T>
		{

		protected AlphaDbListRepo(NpgsqlConnection connection) : base(connection) { }

		public abstract IEnumerable<T> List();
	}
}