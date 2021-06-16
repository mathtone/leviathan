using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.DataAccess.Npgsql {

	public static class NpgsqlCommandExtentions {
		public static NpgsqlCommand WithParameter<T>(this NpgsqlCommand command, string name, T value, ParameterDirection direction, NpgsqlDbType type, int size = default) {
			var p = command.CreateParameter();
			p.ParameterName = name;
			p.Value = value;
			p.Direction = direction;
			p.Size = size;
			p.NpgsqlDbType = type;
			return command.WithParameter(p);
		}

		public static NpgsqlCommand WithInput<T>(this NpgsqlCommand command, string name, T value, NpgsqlDbType type, int size = default) =>
			command.WithParameter(name, value, ParameterDirection.Input, type, size);
	}
}