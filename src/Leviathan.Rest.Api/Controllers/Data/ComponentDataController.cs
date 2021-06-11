using Leviathan.DataAccess;
using Leviathan.Plugins;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Rest.Api.Controllers.Data {

	[Route("Data/[controller]")]
	[ApiController]
	public class ComponentDataController : CrudDataController<long, ComponentRecord, IComponentData> {

		public ComponentDataController(IComponentData data) : base(data) { }

	}
}