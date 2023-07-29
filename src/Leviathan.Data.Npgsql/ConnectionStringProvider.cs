using Leviathan.Services.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Data.Npgsql {

	public class ConnectionStringProvider : LeviathanServiceBase<ConnectionStrings>, IConnectionStringProvider {
		public ConnectionStringProvider(ILogger<ConnectionStringProvider> log, IConfiguration config) :
			base(log, GetConnectionStrings(config)) { }

		public string this[string name] => Config.Values[name];

		static ConnectionStrings GetConnectionStrings(IConfiguration config) =>
			new() {
				Values = config
					.GetSection("ConnectionStrings")?
					.GetChildren()?
					.ToDictionary(k => k.Key, v => v.Value)!
			};
	}

	public interface IConnectionStringProvider {
		string this[string name] { get; }
	}

	public interface IDbConnectionProvider<out CN>
		where CN : IDbConnection {
		CN GetConnection();
	}
}