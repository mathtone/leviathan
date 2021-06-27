using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.DbDataAccess {
	public static class DbDataReaderExtensions {
		public static async Task<T[]> ToArrayAsync<RDR, T>(this Task<RDR> readerTask, Func<IDataRecord, T> selector)
			where RDR : DbDataReader =>

			await readerTask.ConsumeAsync(selector).ToArrayAsync();

		public static async IAsyncEnumerable<T> ConsumeAsync<RDR, T>(this Task<RDR> readerTask, Func<IDataRecord, T> selector)
			where RDR : DbDataReader {

			await foreach (var r in readerTask.Result.ConsumeAsync(selector))
				yield return r;

			await (await readerTask).CloseAsync();
		}

		public static async IAsyncEnumerable<T> ConsumeAsync<RDR, T>(this RDR reader, Func<IDataRecord, T> selector)
			where RDR : DbDataReader {

			while (await reader.ReadAsync())
				yield return selector(reader);

			await reader.CloseAsync();
		}
	}
}