﻿using Npgsql;
using System;
using System.Data;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Leviathan.DbDataAccess.Npgsql {

	public abstract class NpgSqlDataRepository<T> : NpgSqlDataRepository<long, T> {
		protected NpgSqlDataRepository(NpgsqlConnection connection) :
			base(connection) { }
	}

	public abstract class NpgSqlDataRepository<ID, T> : DataRepository<ID, T> {

		//protected IRepoCommands SQL { get; init; }
		protected NpgsqlConnection Connection { get; init; }

		public NpgSqlDataRepository(NpgsqlConnection connection) {
			//this.SQL = commands;
			this.Connection = connection;
		}

		protected NpgsqlConnection Connect() {
			if (this.Connection.State == ConnectionState.Open) {
				return this.Connection;
			}
			for (var i = 0; i < 3; i++) {
				try {
					this.Connection.Open();
					return this.Connection;
				}
				catch(SocketException) {
					//throw ex;
					//HACK
					if (i == 3) {
						throw;
					}
					Thread.Sleep(1000);
				}
			}
			throw new Exception("Connection failure");
		}


		protected async Task<NpgsqlConnection> ConnectAsync() {
			if (this.Connection.State != ConnectionState.Open)
				await this.Connection.OpenAsync();
			return this.Connection;
		}
	}

	public interface IRepoCommands {
		string CREATE { get; }
		string READ { get; }
		string UPDATE { get; }
		string DELETE { get; }
	}

	public interface IListCommands {
		string LIST { get; }
	}
	public interface IListRepoCommands : IListCommands, IRepoCommands {

	}
	public class RepoCommands : IRepoCommands {
		public string CREATE { get; init; }
		public string READ { get; init; }
		public string UPDATE { get; init; }
		public string DELETE { get; init; }
	}

	public class ListRepoCommands : RepoCommands, IListRepoCommands {
		public string LIST { get; init; }
	}
}