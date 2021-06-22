using Leviathan.Alpha.Data.Npgsql;
using Leviathan.SDK;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers.Data {

	public abstract class RepoController<REPO, ID, T> : ControllerBase where REPO : IRepository<ID, T> {
		private ILeviathanAlphaDataContext _context;
		private readonly Func<ILeviathanAlphaDataContext, REPO> _selector;
		private REPO _repo;

		protected ILeviathanAlphaDataContextProvider Provider { get; }
		protected ILeviathanAlphaDataContext CurrentContext => _context ??= Provider.CreateContext();
		protected REPO Repo => _repo ??= _selector(CurrentContext);


		public RepoController(ILeviathanAlphaDataContextProvider provider, Func<ILeviathanAlphaDataContext, REPO> selector) {
			this.Provider = provider;
			this._selector = selector;
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
		public RepoController(ILeviathanAlphaDataContextProvider provider, Func<ILeviathanAlphaDataContext, IRepository<ID, T>> selector) :
			base(provider, selector) {
		}
	}

	public class ListRepoController<ID, T> : ListRepoController<IListRepository<ID, T>, ID, T> {
		public ListRepoController(ILeviathanAlphaDataContextProvider provider, Func<ILeviathanAlphaDataContext, IListRepository<ID, T>> selector) :
			base(provider, selector) { }
	}

	public class ListRepoController<REPO, ID, T> : RepoController<REPO, ID, T>
		where REPO : IRepository<ID, T>, IListing<T> {

		public ListRepoController(ILeviathanAlphaDataContextProvider provider, Func<ILeviathanAlphaDataContext, REPO> selector) :
			base(provider, selector) { }

		[HttpGet, Route("[action]")]
		public IEnumerable<T> List() => Repo.List();
	}
}