using Leviathan.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Leviathan.Rest.Api.Controllers {
	public abstract class CrudListDataController<ID, T, DATA> : CrudDataController<ID, T, DATA>
		where DATA : IListRepository<ID, T> {

		protected CrudListDataController(DATA data) : base(data) { }

		[HttpGet]
		[Route("[action]")]
		public virtual IEnumerable<T> List() => Data.List();
	}

	public abstract class CrudDataController<ID, T, DATA> : ItemDataController<DATA> where DATA : IRepository<T, ID> {

		public CrudDataController(DATA data) : base(data) { }

		[HttpPost]
		public virtual ID Create(T item) => Data.Create(item);

		[HttpDelete]
		[Route("{id:long}")]
		public virtual void Delete(ID id) => Data.Delete(id);

		[HttpGet]
		[Route("{id:long}")]
		public virtual T Read(ID id) => Data.Read(id);

		[HttpPut]
		[Route("{id:long}")]
		public virtual T Update(T item) => Data.Update(item);

	}

	public abstract class ItemDataController<DATA> : ControllerBase {
		protected DATA Data { get; }

		public ItemDataController(DATA data) {
			this.Data = data;
		}
	}

	public abstract class ServiceController<SVC> : ControllerBase {
		protected SVC Service { get; }

		public ServiceController(SVC service) {
			this.Service = service;
		}
	}
}