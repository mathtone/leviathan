
using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace Leviathan.Alpha.Data.Npgsql {

	public interface IComponentAssemblyRepo : IListRepository<long, ComponentAssemblyRecord> {
	}

	public record ComponentAssemblyRecord : StandardEntity<long> {
		public string AssemblyName { get; init; }
		public string AssemblyPath { get; init; }
	}

	public class ComponentAssemblyRepo : AlphaDbListRepo<ComponentAssemblyRecord>, IComponentAssemblyRepo {

		public ComponentAssemblyRepo(NpgsqlConnection connection) :
			base(connection) { }

		public override long Create(ComponentAssemblyRecord item) => Connect()
			.CreateCommand(SQL.CREATE)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@assembly_name", item.AssemblyName)
			.WithInput("@assembly_path", item.AssemblyPath)
			.ExecuteNonQuery();

		public override void Delete(long id) => Connect()
			.CreateCommand(SQL.DELETE)
			.ExecuteNonQuery();

		public override IEnumerable<ComponentAssemblyRecord> List() => Connect()
			.CreateCommand(SQL.LIST)
			.ExecuteReader()
			.ToArray(FromData);

		public override ComponentAssemblyRecord Read(long id) => Connect()
			.CreateCommand(SQL.READ)
			.WithInput("@id", id)
			.ExecuteReadSingle(FromData);

		public override void Update(ComponentAssemblyRecord item) => Connect()
			.CreateCommand(SQL.UPDATE)
			.WithInput("@id", item.Name)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@assembly_name", item.AssemblyName)
			.WithInput("@assembly_path", item.AssemblyPath)
			.ExecuteNonQuery();


		private static ComponentAssemblyRecord FromData(IDataRecord record) => new() {
			Id = record.Field<long>("id"),
			Name = record.Field<string>("name"),
			Description = record.Field<string>("description"),
			AssemblyName = record.Field<string>("assembly_name"),
			AssemblyPath = record.Field<string>("assembly_path"),
		};

		private static readonly IListRepoCommands SQL = new ListRepoCommands {
			CREATE = @"
				INSERT INTO sys.component_assembly (
					name,
					description,
					assembly_name,
					assembly_path
				)
				VALUES(@name, @description, @assembly_name, @assembly_path)
				RETURNING id",

			UPDATE = @"
				UPDATE sys.component_assembly SET
					name=@name,
					description=@description,
					assembly_name=@assembly_name,
					assembly_path=@assembly_path
				WHERE id=@id",

			LIST = @"SELECT * FROM sys.component_assembly",
			READ = @"SELECT * FROM sys.component_assembly WHERE id=@id",
			DELETE = @"DELETE sys.component_assembly WHERE id=@id",
		};
	}
}