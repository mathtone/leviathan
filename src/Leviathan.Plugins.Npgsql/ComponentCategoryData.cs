using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Leviathan.SDK;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.Plugins.Npgsql {
	public class ComponentCategoryData : NpgsqlDataProvider, IComponentCategoryData {

		public ComponentCategoryData(IDbConnectionProvider<NpgsqlConnection> connectionProvider) :
			base(connectionProvider) { }

		public long Create(Category item) => TextCommand(CREATE, c => c
			 .WithInput("@name", item.Name)
			 .WithInput("@description", item.Description)
			 .ExecuteReadSingle(r => r.Field<long>(0))
		);

		public Category Read(long id) => TextCommand(READ, c => c
			.WithInput("@id", id)
			.ExecuteReadSingle(r => new Category {
				Id = r.Field<long>("id"),
				Name = r.Field<string>("name"),
				Description = r.Field<string>("description")
			})
		);

		public Category Update(Category item) => TextCommand(READ, c => c
			 .WithInput("@id", item.Id)
			 .WithInput("@name", item.Name)
			 .WithInput("@description", item.Description)
			 .ExecuteResult(r => r.ExecuteNonQuery(), r => item)
		);

		public void Delete(long id) => TextCommand(
			DELETE, c => c.ExecuteNonQuery()
		);

		public IEnumerable<Category> List() => TextCommand(LIST, c => c
			 .ExecuteReader()
			 .ToArray(r => new Category {
				 Id = r.Field<long>("id"),
				 Name = r.Field<string>("name"),
				 Description = r.Field<string>("description")
			 })
		);

		static readonly string LIST = LoadLocal("Queries.Categories.List.sqlx");
		static readonly string CREATE = LoadLocal("Queries.Categories.Create.sqlx");
		static readonly string READ = LoadLocal("Queries.Categories.Read.sqlx");
		static readonly string UPDATE = LoadLocal("Queries.Categories.Update.sqlx");
		static readonly string DELETE = LoadLocal("Queries.Categories.Delete.sqlx");
	}
}