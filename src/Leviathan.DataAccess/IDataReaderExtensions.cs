using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Leviathan.DataAccess {
	public static class IDataReaderExtensions {


		public static RTN[] ToArray<RDR, RTN>(this RDR reader, Func<IDataRecord, RTN> selector)
			where RDR : IDataReader=>reader.Consume(selector).ToArray();

		public static IEnumerable<RTN> Consume<RDR, RTN>(this RDR reader, Func<IDataRecord, RTN> selector)
			where RDR : IDataReader {

			while (reader.Read())
				yield return selector(reader);

			reader.Close();
		}
	}
}