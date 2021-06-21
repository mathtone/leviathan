using Npgsql;

namespace Leviathan.DbDataAccess.Npgsql {

	public abstract class NpgSqlDataRepository<T> : NpgSqlDataRepository<long, T> {
		protected NpgSqlDataRepository(IRepoCommands commands, NpgsqlConnection connection) :
			base(commands,connection) { }
	}

	public abstract class NpgSqlDataRepository<ID, T> : DataRepository<ID, T> {

		protected IRepoCommands SQL { get; init; }
		protected NpgsqlConnection Connection { get; init; }

		public NpgSqlDataRepository(IRepoCommands commands, NpgsqlConnection connection) {
			this.SQL = commands;
			this.Connection = connection;
		}
	}

	public interface IRepoCommands {
		string CREATE { get; }
		string READ { get; }
		string UPDATE { get; }
		string DELETE { get; }
	}

	public interface IListCommands {
		string LIST { get; }
	}
	public interface IListRepoCommands :IListCommands,IRepoCommands{

	}
	public class RepoCommands : IRepoCommands {
		public string CREATE { get; init; }
		public string READ { get; init; }
		public string UPDATE { get; init; }
		public string DELETE { get; init; }
	}

	public class ListRepoCommands : RepoCommands, IListRepoCommands {
		public string LIST { get; init; }
	}
}