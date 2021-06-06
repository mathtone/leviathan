using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Leviathan.DataAccess;

namespace Leviathan.API.REST.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class ChannelTypeController : ControllerBase {

		ILogger<ChannelTypeController> logger;
		IListRepository<ChannelTypeInfo, int> channelTypes;

		public ChannelTypeController(ILogger<ChannelTypeController> logger, IListRepository<ChannelTypeInfo, int> channelTypes) {

			this.logger = logger;
			this.channelTypes = channelTypes;
		}

		[HttpGet("List")]
		public IEnumerable<ChannelTypeInfo> List() => channelTypes.List();

		[HttpGet("Read")]
		public ChannelTypeInfo Read(int id) => channelTypes.Read(id);

	}
}