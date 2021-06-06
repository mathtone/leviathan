using System;
using System.Data;

namespace Leviathan.DataAccess {
	public class DbDataService<CN, CMD>
		where CN : IDbConnection, new()
		where CMD : IDbCommand {

		protected string connectionString;

		public DbDataService(string connectionString) {
			this.connectionString = connectionString;
		}

		protected void Immediate(string commandText, Action<CMD> action) => Immediate(c => action((CMD)c.CreateCommand(commandText)));
		protected T Immediate<T>(string commandText, Func<CMD, T> action) => Immediate(c => action((CMD)c.CreateCommand(commandText)));
		protected void Immediate(Action<CN> action) => new CN() { ConnectionString = connectionString }.Used(action);
		protected T Immediate<T>(Func<CN, T> action) => new CN() { ConnectionString = connectionString }.Used(action);
	}
}
