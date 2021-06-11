using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Leviathan.Hardware;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.Plugins.Npgsql {

	public class ComponentData : IComponentData {

		protected IDbConnectionProvider<NpgsqlConnection> ConnectionProvider { get; }

		public ComponentData(IDbConnectionProvider<NpgsqlConnection> connectionProvider) {
			this.ConnectionProvider = connectionProvider;
		}

		public IEnumerable<ComponentRecord> List() => ConnectionProvider.Connect()
			.Used(c => c
				.CreateCommand(LIST)
				.ExecuteReader()
				.ToArray(r => new ComponentRecord {
					Id = r.Field<long>("id"),
					Name = r.Field<string>("name"),
					Description = r.Field<string>("description"),
					//Type = new TypeRecord {
					//	TypeName = r.Field<string>("type_name"),
					//	AssemblyName = r.Field<string>("assembly_name"),
					//	AssemblyPath = r.Field<string>("assembly_path")
					//}
				})
			);

		public long Create(Plugins.ComponentRecord item) {
			throw new NotImplementedException();
		}

		public void Delete(long id) {
			throw new NotImplementedException();
		}

		public Plugins.ComponentRecord Read(long id) {
			throw new NotImplementedException();
		}

		public Plugins.ComponentRecord Update(Plugins.ComponentRecord item) {
			throw new NotImplementedException();
		}

		static readonly string LIST = LoadLocal("Queries.Components.List.sqlx");
		//static readonly string CREATE = LoadLocal("Queries.ChannelType.Create.sqlx");
		//static readonly string READ = LoadLocal("Queries.ChannelType.Read.sqlx");
		//static readonly string UPDATE = LoadLocal("Queries.ChannelType.Update.sqlx");
		//static readonly string DELETE = LoadLocal("Queries.ChannelType.Delete.sqlx");
	}
}