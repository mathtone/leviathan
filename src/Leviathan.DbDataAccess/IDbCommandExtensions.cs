using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.DbDataAccess {
	public static class IDbCommandExtensions {

		public static CMD WithTemplate<CMD>(this CMD command, string tag, string value, bool autoEscape = true)
			where CMD : IDbCommand {

			var v = autoEscape ? value : value.Replace("'", "''");
			command.CommandText = command.CommandText.Replace(tag, v);
			return command;
		}

		public static CMD WithInput<CMD, T>(this CMD command, string name, T value, int size = default)
			where CMD : IDbCommand =>
			command.WithParameter(name, value, ParameterDirection.Input, size);

		public static CMD WithOutput<CMD, T>(this CMD command, string name, int size = default)
			where CMD : IDbCommand =>
			command.WithParameter(name, default(T), ParameterDirection.Output, size);

		public static CMD WithInputOutput<CMD, T>(this CMD command, string name, int size = default)
			where CMD : IDbCommand =>
			command.WithParameter(name, default(object), ParameterDirection.InputOutput, size);

		public static CMD WithInputOutput<CMD, T>(this CMD command, string name, T value, int size = default)
			where CMD : IDbCommand =>
			command.WithParameter(name, value, ParameterDirection.InputOutput, size);

		public static CMD WithParameter<CMD, T>(this CMD command, string name, T value, ParameterDirection direction, int size = default)
			where CMD : IDbCommand {

			if (command.Parameters.Contains(name)) {
				var p = command.Parameters[name] as IDbDataParameter;
				p.ParameterName = name;
				p.Value = value;
				p.Direction = direction;
				p.Size = size;
				return command;
			}
			else {
				var p = command.CreateParameter();
				p.ParameterName = name;
				p.Value = value;
				p.Direction = direction;
				p.Size = size;
				return command.WithParameter(p);
			}
		}

		public static CMD WithParameter<CMD, P>(this CMD command, P parameter)
			where CMD : IDbCommand
			where P : IDbDataParameter {
			command.Parameters.Add(parameter);
			return command;
		}

		public static void Execute<CMD>(this CMD command, Action<CMD> executeAction)
			where CMD : IDbCommand {
			executeAction(command);
		}

		public static RSLT ExecuteResult<CMD, ACT, RSLT>(this CMD command, Func<CMD, ACT> executeAction, Func<CMD, ACT, RSLT> resultSelector)
			where CMD : IDbCommand =>
			resultSelector(command, executeAction(command));

		public static RSLT ExecuteResult<CMD, ACT, RSLT>(this CMD command, Func<CMD, ACT> executeAction, Func<ACT, RSLT> resultSelector)
			where CMD : IDbCommand =>
			resultSelector(executeAction(command));

		public static RSLT ExecuteRead<CMD, RSLT>(this CMD command, Func<CMD, IDataReader, RSLT> resultSelector)
			where CMD : IDbCommand => resultSelector(command, command.ExecuteReader());

		public static RSLT ExecuteRead<CMD, RSLT>(this CMD command, Func<IDataReader, RSLT> resultSelector)
			where CMD : IDbCommand {
			using var rdr = command.ExecuteReader();
			return resultSelector(rdr);
		}

		public static RSLT ExecuteReadSingle<CMD, RSLT>(this CMD command, Func<IDataRecord, RSLT> resultSelector)
			where CMD : IDbCommand =>
			ExecuteRead(command, r => r.Consume(resultSelector).Single());


		//public static IEnumerable<RSLT> ExecuteReadAll<CMD, RSLT>(this CMD command, Func<IDataRecord, RSLT> resultSelector)
		//	where CMD : IDbCommand => ExecuteRead(command).Consume(resultSelector);

	}

	public static class DbCommandExtensions {
		public static async Task ExecuteAsync<CMD>(this CMD command, Func<CMD, Task> executeAction)
			where CMD : DbCommand {
			await executeAction(command);
		}

		public static async Task<RSLT> ExecuteResultAsync<CMD, ACT, RSLT>(this CMD command, Func<CMD, ACT> executeAction, Func<CMD, ACT, Task<RSLT>> resultSelector)
			where CMD : DbCommand =>
			await resultSelector(command, executeAction(command));

		public static async Task<RSLT> ExecuteResult<CMD, ACT, RSLT>(this CMD command, Func<CMD, ACT> executeAction, Func<ACT, Task<RSLT>> resultSelector)
			where CMD : DbCommand =>
			await resultSelector(executeAction(command));

		public static async Task<RSLT> ExecuteReadAsync<CMD, RSLT>(this CMD command, Func<CMD, DbDataReader, Task<RSLT>> resultSelector)
			where CMD : DbCommand => await resultSelector(command, await command.ExecuteReaderAsync());

		public static async Task<RSLT> ExecuteReadAsync<CMD, RSLT>(this CMD command, Func<DbDataReader, Task<RSLT>> resultSelector)
			where CMD : DbCommand {
			using var rdr = await command.ExecuteReaderAsync();
			return await resultSelector(rdr);
		}

		public static async Task<RSLT> ExecuteReadSingleAsync<CMD, RSLT>(this CMD command, Func<IDataRecord, RSLT> resultSelector)
			where CMD : DbCommand =>
				await ExecuteReadAsync(command, async r => await r.ConsumeAsync(resultSelector).SingleAsync());
	}
}