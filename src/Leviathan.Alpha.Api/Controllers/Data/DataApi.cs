using Leviathan.Alpha.Data.Npgsql;
using Leviathan.SDK;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers.Data {

	[ApiController,Route("api/data/[controller]")]
	public class ComponentAssemblyController : ListRepoController<long,ComponentAssemblyRecord> {
		public ComponentAssemblyController(IListRepository<long, ComponentAssemblyRecord> repository) : base(repository) { }
	}

	[ApiController, Route("api/data/[controller]")]
	public class ComponentCategoryController : ListRepoController<long, ComponentCategoryRecord> {
		public ComponentCategoryController(IListRepository<long, ComponentCategoryRecord> repository) : base(repository) {}
	}

	[ApiController, Route("api/data/[controller]")]
	public class ComponentTypeController : ListRepoController<long, ComponentTypeRecord> {
		public ComponentTypeController(IListRepository<long, ComponentTypeRecord> repository) : base(repository) { }
	}

	[ApiController, Route("api/data/[controller]")]
	public class HardwareConnectorController : ListRepoController<long, HardwareConnectorRecord> {
		public HardwareConnectorController(IListRepository<long, HardwareConnectorRecord> repository) : base(repository) { }
	}

	[ApiController, Route("api/data/[controller]")]
	public class HardwareModuleController : ListRepoController<long, HardwareModuleRecord> {
		public HardwareModuleController(IListRepository<long, HardwareModuleRecord> repository) : base(repository) { }
	}
}