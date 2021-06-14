using System;
using System.Data;

namespace Leviathan.DataAccess {

	public interface IDbConnectionProvider : IDbConnectionProvider<IDbConnection> {
		
	}
	//public interface IDbConnectionProvider<I> {

	//}


	public interface IDbConnectionProvider<out CN> where CN : IDbConnection {
		CN Connect();
		void SetConnectionInfo(string connectionInfo);
	}

	public interface IDbConnectionProvider<out CN, I> : IDbConnectionProvider where CN : IDbConnection {
		CN Connect(I connectionData);
	}

	public class DbDataProvider<CN, CMD> : DbDataProvider<CN, CMD, IDbConnectionProvider<CN>>
		where CN : IDbConnection
		where CMD : IDbCommand {

		public DbDataProvider(IDbConnectionProvider<CN> provider) : base(provider) {
		}
	}

	public class DbDataProvider<CN, CMD, PROVIDER>
		where CN : IDbConnection
		where CMD : IDbCommand
		where PROVIDER : IDbConnectionProvider<CN> {

		protected PROVIDER Connections { get; }

		public DbDataProvider(PROVIDER provider) {
			this.Connections = provider;
		}

		protected void TextCommand(string command, Action<CMD> action) =>
			Command(command, CommandType.Text, action);

		protected T TextCommand<T>(string command, Func<CMD, T> action) =>
			Command(command, CommandType.Text, action);

		protected void Command(string command, CommandType type, Action<CMD> action) =>
			Connected(c => action(IDbConnectionExtensions.CreateCommand<CN, CMD>(c, command, type)));

		protected T Command<T>(string command, CommandType type, Func<CMD, T> action) =>
			Connected(c => action(IDbConnectionExtensions.CreateCommand<CN, CMD>(c, command, type)));

		protected void Connected(Action<CN> action) => Connections.Connect().Used(action);
		protected T Connected<T>(Func<CN, T> action) => Connections.Connect().Used(action);
	}
}