using Leviathan.Alpha.Data.Npgsql;
using Leviathan.SDK;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers.Data {

	public class RepoController<REPO, ID, T> : ControllerBase where REPO : IRepository<ID, T> {

		protected REPO Repo { get; }

		public RepoController(REPO repository) {
			this.Repo = repository;
		}

		[HttpPost]
		public ID Create(T item) => Repo.Create(item);

		[HttpDelete]
		public void Delete(ID id) => Repo.Delete(id);

		[HttpGet]
		public T Read(ID id) => Repo.Read(id);

		[HttpPut]
		public void Update(T item) => Repo.Update(item);
	}

	public class RepoController<ID, T> : RepoController<IRepository<ID, T>, ID, T> {
		public RepoController(IRepository<ID, T> repository) : base(repository) {
		}
	}

	public class ListRepoController<ID, T> : ListRepoController<IListRepository<ID, T>, ID, T> {
		public ListRepoController(IListRepository<ID, T> repository) : base(repository) { }

	}

	public class ListRepoController<REPO, ID, T> : RepoController<REPO, ID, T>
		where REPO : IRepository<ID, T>, IListing<T> {
		public ListRepoController(REPO repository) : base(repository) { }

		[HttpGet, Route("[action]")]
		public IEnumerable<T> List() => Repo.List();
	}
}