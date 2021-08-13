using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace TheLeviathan.ComponentSystem {
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
