using System;
using System.Data;

namespace Leviathan.DbDataAccess {
	public static class IDataRecordExtensions {
		public static object Get(this IDataRecord record, string name) => record.Get<object>(name);
		public static object Get(this IDataRecord record, int index) => record.Get<string>(index);
		public static T Get<T>(this IDataRecord record, string name) => (T)Convert.ChangeType(record[name], typeof(T));
		public static T Get<T>(this IDataRecord record, int index) => (T)Convert.ChangeType(record[index], typeof(T));
	}
}