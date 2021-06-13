using Leviathan.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Leviathan.Rest.Api.Controllers {

	public abstract class CrudListDataController<T, DATA> : CrudListDataController<long, T, DATA>
		where DATA : IListRepository<long, T> {

		protected CrudListDataController(DATA data) : base(data) { }
	}

	public abstract class CrudListDataController<ID, T, DATA> : CrudDataController<ID, T, DATA>, IListRepository<ID, T>
		where DATA : IListRepository<ID, T> {

		protected CrudListDataController(DATA data) : base(data) { }

		[HttpGet]
		public virtual IEnumerable<T> List() => Data.List();
	}
}