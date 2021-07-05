using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Data.Npgsql {
	public interface IRemotePartnerRepo : IListRepository<long, RemotePartnerRecord> {
	}
	public record RemotePartnerRecord : StandardEntity<long> {
		public string ApiUrl { get; init; }
	}

	public class RemotePartnerRepo : AlphaDbListRepo<RemotePartnerRecord>, IRemotePartnerRepo{
		public RemotePartnerRepo(NpgsqlConnection connection) : base(connection) {
		}

		public override long Create(RemotePartnerRecord item) => Connect()
			.CreateCommand(SQL.CREATE)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@api_url", item.ApiUrl)
			.ExecuteReadSingle(r => r.Get<long>("id"));

		public override void Delete(long id) => Connect()
			.CreateCommand(SQL.DELETE)
			.ExecuteNonQuery();

		public override IEnumerable<RemotePartnerRecord> List() => Connect()
			.CreateCommand(SQL.LIST)
			.ExecuteReader()
			.ToArray(FromData);

		public override RemotePartnerRecord Read(long id) => Connect()
			.CreateCommand(SQL.READ)
			.WithInput("@id", id)
			.ExecuteReadSingle(FromData);

		public override void Update(RemotePartnerRecord item) => Connect()
			.CreateCommand(SQL.UPDATE)
			.WithInput("@id", item.Name)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@api_url", item.ApiUrl)
			.ExecuteNonQuery();

		private static RemotePartnerRecord FromData(IDataRecord record) => new() {
			Id = record.Get<long>("id"),
			Name = record.Get<string>("name"),
			Description = record.Get<string>("description"),
			ApiUrl = record.Get<string>("api_url")
		};

		private static readonly IListRepoCommands SQL = new ListRepoCommands {
			CREATE = @"
				INSERT INTO sys.remote_partner (
					name,
					description,
					api_url
				)
				VALUES(
					@name,
					@description,
					@api_url
				)
				RETURNING id",

			UPDATE = @"
				UPDATE sys.remote_partner SET
					name=@name,
					description=@description,
					api_url=@api_url
				WHERE id=@id",

			LIST = "SELECT * FROM sys.remote_partner",
			READ = "SELECT * FROM sys.remote_partner WHERE id=@id",
			DELETE = "DELETE sys.remote_partner WHERE id=@id",
		};
	}
}