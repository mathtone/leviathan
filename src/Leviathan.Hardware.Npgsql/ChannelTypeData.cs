using Leviathan.Utilities;
using Npgsql;
using System;
using System.Data;
//using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using System.Collections.Generic;
using Leviathan.DataAccess;
using System.Linq;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.Hardware.Npgsql {

	public class ChannelTypeData : IChannelTypeData {

		protected IDbConnectionProvider<NpgsqlConnection> ConnectionProvider { get; }

		public ChannelTypeData(IDbConnectionProvider<NpgsqlConnection> connectionProvider) {
			this.ConnectionProvider = connectionProvider;
		}

		public long Create(TypeRecord item) {
			throw new NotImplementedException();
		}

		public void Delete(long id) {
			throw new NotImplementedException();
		}

		public TypeRecord Read(long id) {
			throw new NotImplementedException();
		}

		public TypeRecord Update(TypeRecord item) {
			throw new NotImplementedException();
		}

		public IEnumerable<TypeRecord> List() => ConnectionProvider.Connect().Used(c => c
			.CreateCommand(LIST)
			.ExecuteReader()
			.ToArray(r => new TypeRecord {
				//Id = r.Field<long>("id"),
				//Name = r.Field<string>("name")
			})
		);

		static readonly string LIST = LoadLocal("Queries.ChannelType.Catalog.sqlx");
		static readonly string CREATE = LoadLocal("Queries.ChannelType.Create.sqlx");
		static readonly string READ = LoadLocal("Queries.ChannelType.Read.sqlx");
		static readonly string UPDATE = LoadLocal("Queries.ChannelType.Update.sqlx");
		static readonly string DELETE = LoadLocal("Queries.ChannelType.Delete.sqlx");

	}
}