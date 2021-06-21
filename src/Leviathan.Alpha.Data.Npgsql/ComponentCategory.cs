using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Data.Npgsql {

	public record ComponentCategoryRecord : StandardEntity<long> { }

	public interface IComponentCategoryRepo : IListRepository<long, ComponentCategoryRecord> { }

	public class QuickRepo<T> { }

	public class ComponentCategoryRepo : AlphaDbListRepo<ComponentCategoryRecord>, IComponentCategoryRepo {

		public ComponentCategoryRepo(NpgsqlConnection connection) : base(connection) { }

		public override long Create(ComponentCategoryRecord item) => Connect()
			.CreateCommand(SQL.CREATE)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.ExecuteNonQuery();

		public override void Delete(long id) => Connect()
			.CreateCommand(SQL.DELETE)
			.ExecuteNonQuery();

		public override IEnumerable<ComponentCategoryRecord> List() => Connect()
			.CreateCommand(SQL.LIST)
			.ExecuteReader()
			.ToArray(FromData);

		public override ComponentCategoryRecord Read(long id) => Connect()
			.CreateCommand(SQL.READ)
			.WithInput("@id", id)
			.ExecuteReadSingle(FromData);

		public override void Update(ComponentCategoryRecord item) => Connect()
			.CreateCommand(SQL.UPDATE)
			.WithInput("@id", item.Name)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.ExecuteNonQuery();

		private static ComponentCategoryRecord FromData(IDataRecord record) => new() {
			Id = record.Field<long>("id"),
			Name = record.Field<string>("name"),
			Description = record.Field<string>("description"),
		};

		private static readonly IListRepoCommands SQL = new ListRepoCommands {
			CREATE = @"
				INSERT INTO sys.component_category (
					name,
					description
				)
				VALUES(@name,@description)
				RETURNING id",

			UPDATE = @"
				UPDATE sys.component_category
				SET name=@name,description=@description
				WHERE id=@id",

			LIST = "SELECT * FROM sys.component_category",
			READ = "SELECT * FROM sys.component_category WHERE id=@id",
			DELETE = "DELETE sys.component_category WHERE id=@id",
		};
	}
}