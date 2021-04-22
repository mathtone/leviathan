using System;
using System.Data;

namespace Leviathan.DataAccess {
	public static class IDataRecordExtensions {

		public static string Field(this IDataRecord record, string name) => record.Field<string>(name);
		public static string Field(this IDataRecord record, int index) => record.Field<string>(index);
		public static T Field<T>(this IDataRecord record, string name) => (T)Convert.ChangeType(record[name], typeof(T));
		public static T Field<T>(this IDataRecord record, int index) => (T)Convert.ChangeType(record[index], typeof(T));

	}
}