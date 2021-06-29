using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace Leviathan.Alpha.Data.Npgsql {

	public record ComponentTypeRecord : StandardEntity<long> {
		public long CategoryId { get; init; }
		public long AssemblyId { get; init; }
		public string TypeName { get; init; }
		public string TypeLocator { get; init; }
	}

	public interface IComponentTypeRepo : IListRepository<long, ComponentTypeRecord> { }
	public class ComponentTypeRepo : AlphaDbListRepo<ComponentTypeRecord>, IComponentTypeRepo {
		public ComponentTypeRepo(NpgsqlConnection connection) :
			base(connection) {
		}

		public override long Create(ComponentTypeRecord item) => Connect()
			.CreateCommand(SQL.CREATE)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@component_assembly_id", item.AssemblyId)
			.WithInput("@component_category_id", item.CategoryId)
			.WithInput("@type_name", item.TypeName)
			.WithInput("@type_locator", item.TypeLocator)
			.ExecuteReadSingle(r=>r.Get<long>("id"));

		public override void Delete(long id) => Connect()
			.CreateCommand(SQL.DELETE)
			.ExecuteNonQuery();

		public override IEnumerable<ComponentTypeRecord> List() => Connect()
			.CreateCommand(SQL.LIST)
			.ExecuteReader()
			.ToArray(FromData);

		public override ComponentTypeRecord Read(long id) => Connect()
			.CreateCommand(SQL.READ)
			.WithInput("@id", id)
			.ExecuteReadSingle(FromData);

		public override void Update(ComponentTypeRecord item) => Connect()
			.CreateCommand(SQL.UPDATE)
			.WithInput("@id", item.Id)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@component_assembly_id", item.AssemblyId)
			.WithInput("@component_category_id", item.CategoryId)
			.WithInput("@type_name", item.TypeName)
			.WithInput("@type_locator", item.TypeLocator)
			.ExecuteNonQuery();

		private static ComponentTypeRecord FromData(IDataRecord record) => new() {
			Id = record.Get<long>("id"),
			Name = record.Get<string>("name"),
			Description = record.Get<string>("description"),
			AssemblyId = record.Get<long>("component_assembly_id"),
			CategoryId = record.Get<long>("component_category_id"),
			TypeName = record.Get<string>("type_name"),
			TypeLocator = record.Get<string>("type_locator")
		};

		private static readonly IListRepoCommands SQL = new ListRepoCommands {
			CREATE = @"
				INSERT INTO sys.component_type (
					name,
					description,
					component_category_id,
					component_assembly_id,
					type_name,
					type_locator
				)
				VALUES(
					@name,
					@description,
					@component_category_id,
					@component_assembly_id,
					@type_name,
					@type_locator
				)
				RETURNING id",

			UPDATE = @"
				UPDATE sys.component_type SET 
					name=@name,
					description=@description,
					component_category_id=@component_category_id,
					component_assembly_id=@component_assembly_id,
	`				type_name=@type_name,
					type_locator=@type_locator

				WHERE id=@id",
			LIST = @"SELECT * FROM sys.component_type",
			READ = @"SELECT * FROM sys.component_type WHERE id=@id",
			DELETE = @"DELETE sys.component_type WHERE id=@id",
		};
	}
}