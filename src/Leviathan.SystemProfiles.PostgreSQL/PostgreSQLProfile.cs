using Leviathan.Alpha.NpgsqlDataSystem;
using Leviathan.SystemConfiguration.SDK;
using System;
using System.Threading.Tasks;

namespace Leviathan.SystemProfiles.PostgreSQL {

	[SystemProfile]
	public class PostgreSQLProfile : SystemProfileComponent {

		INpgsqlDataSystem _data;

		[ProfileProperty("Host Name", "Database server host name.")]
		public string HostName { get; set; }

		[ProfileProperty("Instance DB Name", "Database name.")]
		public string DbName { get; set; }

		[ProfileProperty("DB Login", "Database login.")]
		public string Login { get; set; }

		[ProfileProperty("DB Password", "Database password.")]
		public string Password { get; set; }

		public PostgreSQLProfile(INpgsqlDataSystem data) {
			this._data = data;
		}

		public async override Task Apply() {
			await Task.CompletedTask;
			;
		}
	}
}