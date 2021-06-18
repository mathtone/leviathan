using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Leviathan.Sandbox {
	public class InitializationTests {

		[Fact]
		public async void ConnectTest() {
			var cn = new NpgsqlConnection($"Host=poseidonalpha.local;Username=pi;Database=postgres;Password=Digital!2021;");
			cn.Open();
			var r = await cn.CreateCommand("SELECT datname FROM pg_database;")
				.ExecuteReaderAsync()
				.ConsumeAsync(r => r.GetString(0))
				.ToArrayAsync();

			cn.Close();

		}
	}

	

	//public static class DataReaderExtensions {
	//	public static async IAsyncEnumerable<T> ConsumeAsync<T>(this Task<NpgsqlDataReader> reader, Func<IDataRecord, T> selector) {
	//		while (await reader.ReadAsync())
	//			yield return selector(reader);
	//	}
	//	//public static async IAsyncEnumerable<RTN> ConsumeAsync<RDR, RTN>(this RDR reader, Func<IDataRecord, RTN> selector)
	//	//	where RDR : DbDataReader {

	//	//	while (await reader.ReadAsync())
	//	//		yield return selector(reader);

	//	//	reader.Close();
	//	//}
	//	//public static RTN[] ToArray<RDR, RTN>(this RDR reader, Func<IDataRecord, RTN> selector)
	//	//	where RDR : IDataReader => reader.Consume(selector).ToArray();

	//	//public static IEnumerable<RTN> Consume<RDR, RTN>(this RDR reader, Func<IDataRecord, RTN> selector)
	//	//	where RDR : IDataReader {

	//	//	while (reader.Read())
	//	//		yield return selector(reader);

	//	//	reader.Close();
	//	//}
	//}

	public static class DbConnectionExtensions {

		public static CMD CreateCommand<CN, CMD>(this CN connection, string commandText, CommandType type = CommandType.Text)
			where CN : IDbConnection
			where CMD : IDbCommand {

			var rtn = connection.CreateCommand();
			rtn.CommandText = commandText;
			rtn.CommandType = type;
			return (CMD)rtn;
		}

		public static async Task UsedAsync<CN>(this CN connection, Action<CN> action) where CN : DbConnection {
			try {
				await connection.OpenAsync();
				action(connection);
			}
			finally {
				connection.Close();
				connection.Dispose();
			}
		}

		public static T Used<T, CN>(this CN connection, Func<CN, T> resultSelector) where CN : IDbConnection {
			try {
				connection.Open();
				return resultSelector(connection);
			}
			finally {
				connection.Close();
				connection.Dispose();
			}
		}
	}
}