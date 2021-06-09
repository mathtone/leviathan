using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Rest.Api.Controllers {

	[ApiController]
	[Route("[controller]/[action]")]
	public class HardwareModuleTypeController : ItemDataController<IHardwareModuleTypeData> {

		public HardwareModuleTypeController(IHardwareModuleTypeData data) : base(data) { }

		[HttpGet]
		public IEnumerable<TypeInfo> Catalog() => Data.Catalog();

		[HttpPost]
		public long Create(TypeInfo type) => Data.Create(type);

		[HttpGet]
		public TypeInfo Read(long id) => Data.Read(id);

		[HttpPut]
		public void Update(TypeInfo type) => Data.Update(type);

		[HttpDelete]
		public void Delete(long id) => Data.Delete(id);
	}
}