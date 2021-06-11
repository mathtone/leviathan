using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Npgsql;
using System;
using System.Collections.Generic;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.Plugins.Npgsql {
	public class ComponentCategoryData : IComponentCategoryData {

		protected IDbConnectionProvider<NpgsqlConnection> ConnectionProvider { get; }

		public ComponentCategoryData(IDbConnectionProvider<NpgsqlConnection> connectionProvider) {
			this.ConnectionProvider = connectionProvider;
		}

		public long Create(Category item) => ConnectionProvider.Connect().Used(c => c
			.CreateCommand(CREATE)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.ExecuteReadSingle(r => r.Field<long>(0))
		);

		public Category Read(long id) => ConnectionProvider.Connect().Used(c => c
			.CreateCommand(READ)
			.WithInput("@id", id)
			.ExecuteReadSingle(r => new Category {
				Id = r.Field<long>("id"),
				Name = r.Field<string>("name"),
				Description = r.Field<string>("description")
			})
		);

		public Category Update(Category item) => ConnectionProvider.Connect().Used(c => c
			.CreateCommand(READ)
			.WithInput("@id", item.Id)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.ExecuteResult(r => r.ExecuteNonQuery(), r => item)
		);

		public void Delete(long id) => ConnectionProvider.Connect().Used(c => c
			.CreateCommand(DELETE)
			.ExecuteNonQuery()
		);

		public IEnumerable<Category> List() => ConnectionProvider.Connect().Used(c => c
			.CreateCommand(LIST)
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