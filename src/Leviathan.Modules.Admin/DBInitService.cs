using System;
using System.Data;
using System.Threading.Tasks;
using Leviathan.DataAccess;

namespace Leviathan.Modules.Admin {

	public interface IDBInitData {
		void DropDB(string dbName);
		bool LocateDB(string dbName);
		void CreateDB(string dbName);
		void InitialzeDB(string dbName);

	}

	public class DBInitService {

		readonly string dbName;
		readonly IDBInitData data;

		public DBInitService(IDBInitData data, string dbName) {
			this.data = data;
			this.dbName = dbName;
		}

		public async Task Initialize(string dbName = null, bool force = false) => await Task.Run(() => {
			var name = dbName ?? this.dbName;
			var exists = data.LocateDB(name);
			
			//if (exists && force) {
			data.DropDB(dbName);
			//}
			if (!exists) {
				data.CreateDB(name);
				data.InitialzeDB(name);
			}
		});
	}
}