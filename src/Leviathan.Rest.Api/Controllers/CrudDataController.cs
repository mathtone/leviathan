using Leviathan.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Leviathan.Rest.Api.Controllers {

	public abstract class CrudDataController<T, DATA> : CrudDataController<long, T, DATA> where DATA : IRepository<T, long> {
		public CrudDataController(DATA data) : base(data) { }
	}

	public abstract class CrudDataController<ID, T, DATA> : ItemDataController<DATA>, IRepository<T, ID>
		where DATA : IRepository<T, ID> {

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
}