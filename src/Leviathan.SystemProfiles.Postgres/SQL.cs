using Leviathan.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.SystemProfiles.Postgres {
	public static class SQL {

		public static class DB {
			public static readonly string Create = ResourceLoader.LoadLocal("SQL.DB.Create.sqlx");
			public static readonly string Drop = ResourceLoader.LoadLocal("SQL.DB.Drop.sqlx");
			public static readonly string Locate = ResourceLoader.LoadLocal("SQL.DB.Locate.sqlx");
			public static readonly string Init = ResourceLoader.LoadLocal("SQL.DB.Init.sqlx");
			public static readonly string InitData = ResourceLoader.LoadLocal("SQL.DB.InitData.sqlx");
		}
	}
}
