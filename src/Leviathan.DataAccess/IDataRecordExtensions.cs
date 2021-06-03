using System;
using System.Data;

namespace Leviathan.DataAccess {
	public static class IDataRecordExtensions {

		public static object Field(this IDataRecord record, string name) => record.Field<object>(name);
		public static object Field(this IDataRecord record, int index) => record.Field<string>(index);
		public static T Field<T>(this IDataRecord record, string name) => (T)Convert.ChangeType(record[name], typeof(T));
		public static T Field<T>(this IDataRecord record, int index) => (T)Convert.ChangeType(record[index], typeof(T));

	}
}