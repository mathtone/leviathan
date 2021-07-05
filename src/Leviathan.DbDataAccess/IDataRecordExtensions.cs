using System;
using System.Data;

namespace Leviathan.DbDataAccess {
	public static class IDataRecordExtensions {
		public static object Get(this IDataRecord record, string name) => record.Get<object>(name);
		public static object Get(this IDataRecord record, int index) => record.Get<string>(index);
		public static T Get<T>(this IDataRecord record, string name) => 
				ChangeType<T>(record[name] == DBNull.Value?default:record[name]);

		public static T Get<T>(this IDataRecord record, int index) => ChangeType<T>(record[index] == DBNull.Value ? default : record[index]);

		public static T ChangeType<T>(object value) {
			var t = typeof(T);

			if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) {
				if (value == null) {
					return default;
				}

				t = Nullable.GetUnderlyingType(t);
			}

			return (T)Convert.ChangeType(value, t);
		}
	}
}