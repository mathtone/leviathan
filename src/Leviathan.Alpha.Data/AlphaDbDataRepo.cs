using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Npgsql;
using System.Collections.Generic;

namespace Leviathan.Alpha.Data {
	public abstract class AlphaDbDataRepo<T> : AlphaDbDataRepo<T, IRepoCommands> {
		protected AlphaDbDataRepo(IRepoCommands commands, NpgsqlConnection connection) : base(commands,connection) {
		}
	}
	public abstract class AlphaDbDataRepo<T, CMD> : AlphaDbDataRepo<long, T, CMD> where CMD : IRepoCommands {
		protected AlphaDbDataRepo(CMD commands, NpgsqlConnection connection) :
			base(commands,connection) { }
	}

	public abstract class AlphaDbDataRepo<ID, T, CMD> : NpgSqlDataRepository<ID, T> where CMD : IRepoCommands {
		protected AlphaDbDataRepo(CMD commands,NpgsqlConnection connection) :
			base(commands,connection) { }
	}

	public abstract class AlphaDbListRepo<T> : AlphaDbListRepo<long, T, IListRepoCommands> {
		protected AlphaDbListRepo(IListRepoCommands commands, NpgsqlConnection connection) : base(commands,connection) { }
	}

	public abstract class AlphaDbListRepo<ID, T> : AlphaDbListRepo<ID, T, IListRepoCommands> {
		protected AlphaDbListRepo(IListRepoCommands commands, NpgsqlConnection connection) : base(commands,connection) { }
	}

	public abstract class AlphaDbListRepo<ID, T, CMD> : AlphaDbDataRepo<ID, T, CMD>, IListRepository<ID, T>
		where CMD : IListRepoCommands {

		protected AlphaDbListRepo(CMD commands,NpgsqlConnection connection) : base(commands,connection) { }

		public abstract IEnumerable<T> List();
	}
}