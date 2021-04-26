using System;
using System.Collections.Generic;
using System.Data;

namespace Leviathan.DataAccess {

	public static class IDataReaderExtensions {

		//public static IEnumerable<RTN> Consume<RTN>(this IDataReader reader, Func<IDataRecord, RTN> selector) =>
		//	reader.Consume(selector, r => r.Dispose());
		//public static IEnumerable<RTN> Consume<RDR, RTN>(this RDR reader, Func<RDR, RTN> selector, Action<RDR> onComplete)
		public static IEnumerable<RTN> Consume<RDR, RTN>(this RDR reader, Func<IDataRecord, RTN> selector)
			where RDR : IDataReader {

			while (reader.Read())
				yield return selector(reader);

			reader.Close();
		}
	}
}