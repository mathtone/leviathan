namespace Leviathan.DbDataAccess.Npgsql {

	public class NpgSqlDataRepository<T> : NpgSqlDataRepository<long, T> {

	}

	public class NpgSqlDataRepository<ID, T> : DataRepository<ID, T> {
		public override ID Create(T item) {
			throw new System.NotImplementedException();
		}

		public override void Delete(ID id) {
			throw new System.NotImplementedException();
		}

		public override T Read(ID id) {
			throw new System.NotImplementedException();
		}

		public override void Update(T item) {
			throw new System.NotImplementedException();
		}
	}
}