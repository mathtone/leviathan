using Leviathan.Common;
using Leviathan.Components.Sdk;
using Leviathan.DataAccess;
using Leviathan.WebApi;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheLeviathan.ComponentSystem.Data {
	[ApiComponent("data")]
	public class AssemblyDataController : ControllerBase, IAssemblyData {

		IAssemblyData _service;

		public AssemblyDataController(IAssemblyData service) => _service = service;

		[HttpPost]
		public Task<int> CreateAsync(AssemblyRecord item) => _service.CreateAsync(item);

		[HttpGet]
		public Task<AssemblyRecord> ReadAsync(int id) {
			throw new System.NotImplementedException();
		}

		[HttpPut]
		public Task UpdateAsync(AssemblyRecord item) {
			throw new System.NotImplementedException();
		}

		[HttpDelete]
		public Task DeleteAsync(int id) {
			throw new System.NotImplementedException();
		}

		[HttpGet, Route("[action]")]
		public IEnumerable<AssemblyRecord> List() => _service.List();
	}
}