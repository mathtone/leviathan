using Leviathan.Alpha.Data.Npgsql;
using Leviathan.SDK;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers.Data {

	[ApiController, Route("api/data/[controller]")]
	public class ComponentAssemblyController : ListRepoController<long, ComponentAssemblyRecord> {
		public ComponentAssemblyController(ILeviathanAlphaDataContextProvider context) :
			base(context, r => r.ComponentAssembly) { }
	}

	[ApiController, Route("api/data/[controller]")]
	public class ComponentCategoryController : ListRepoController<long, ComponentCategoryRecord> {
		public ComponentCategoryController(ILeviathanAlphaDataContextProvider context) :
			base(context, r => r.ComponentCategory) { }
	}

	[ApiController, Route("api/data/[controller]")]
	public class ComponentTypeController : ListRepoController<long, ComponentTypeRecord> {
		public ComponentTypeController(ILeviathanAlphaDataContextProvider context) :
			base(context, r => r.ComponentType) { }
	}

	[ApiController, Route("api/data/[controller]")]
	public class HardwareConnectorController : ListRepoController<long, HardwareConnectorRecord> {
		public HardwareConnectorController(ILeviathanAlphaDataContextProvider context) :
			base(context, r => r.HardwareConnector) { }
	}

	[ApiController, Route("api/data/[controller]")]
	public class HardwareModuleController : ListRepoController<long, HardwareModuleRecord> {
		public HardwareModuleController(ILeviathanAlphaDataContextProvider context) :
			base(context, r => r.HardwareModule) { }
	}

	[ApiController, Route("api/data/[controller]")]
	public class HardwareChannelController : ListRepoController<long, HardwareChannelRecord> {
		public HardwareChannelController(ILeviathanAlphaDataContextProvider context) :
			base(context, r => r.HardwareChannel) { }
	}
}