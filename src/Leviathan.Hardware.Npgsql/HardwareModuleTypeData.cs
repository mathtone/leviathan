using Npgsql;
using System;
using System.Data;
//using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using static Leviathan.Utilities.ResourceLoader;
using System.Collections.Generic;
using Leviathan.DataAccess;
using System.Linq;

namespace Leviathan.Hardware.Npgsql {
	public class HardwareModuleTypeData : IHardwareModuleTypeData {

		protected IDbConnectionProvider<NpgsqlConnection> ConnectionProvider { get; }

		public HardwareModuleTypeData(IDbConnectionProvider<NpgsqlConnection> connectionProvider) {
			this.ConnectionProvider = connectionProvider;
		}

		public IEnumerable<TypeRecord> List() => ConnectionProvider.Connect().Used(c => c
			.CreateCommand(LIST)
			.ExecuteReader()
			.ToArray(r => new TypeRecord {
				//Id = r.Field<long>("id"),
				//Name = r.Field<string>("name")
			})
		);

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

		static readonly string LIST = LoadLocal("Queries.HardwareModuleType.Catalog.sqlx");
		static readonly string CREATE = LoadLocal("Queries.HardwareModuleType.Create.sqlx");
		static readonly string READ = LoadLocal("Queries.HardwareModuleType.Read.sqlx");
		static readonly string UPDATE = LoadLocal("Queries.HardwareModuleType.Update.sqlx");
		static readonly string DELETE = LoadLocal("Queries.HardwareModuleType.Delete.sqlx");
	}
}