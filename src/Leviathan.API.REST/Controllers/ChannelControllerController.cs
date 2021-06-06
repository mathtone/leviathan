using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Leviathan.DataAccess;
using Leviathan.Services.Core.Hardware;

namespace Leviathan.API.REST.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class ChannelControllerController : LeviathanDataController<IChannelControllerData> {

		ILogger<ChannelControllerController> logger;

		public ChannelControllerController(ILogger<ChannelControllerController> logger, IChannelControllerData data) : base(data) {
			this.logger = logger;			
		}

		[HttpGet("List")]
		public IEnumerable<ChannelControllerInfo> List() => Data.List();

		[HttpGet("Read")]
		public ChannelControllerInfo Read(int id) => Data.Read(id);

		[HttpGet("Catalog")]
		public IEnumerable<ChannelControllerCatalogItem> Catalog() => Data.Catalog();

	}
}

