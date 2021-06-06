using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Leviathan.DataAccess;

namespace Leviathan.API.REST.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class ChannelControllerTypeController : ControllerBase {

		ILogger<ChannelControllerTypeController> logger;
		IListRepository<ChannelControllerTypeInfo, int> channelControllerTypes;

		public ChannelControllerTypeController(ILogger<ChannelControllerTypeController> logger, IListRepository<ChannelControllerTypeInfo, int> channelControllerTypes) {

			this.logger = logger;
			this.channelControllerTypes = channelControllerTypes;
		}

		[HttpGet("List")]
		public IEnumerable<ChannelControllerTypeInfo> List() => channelControllerTypes.List();

		[HttpGet("Read")]
		public ChannelControllerTypeInfo Read(int id) => channelControllerTypes.Read(id);

	}
}

