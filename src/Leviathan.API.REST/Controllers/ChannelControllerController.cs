using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Leviathan.DataAccess;

namespace Leviathan.API.REST.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class ChannelControllerController : ControllerBase {

		ILogger<ChannelControllerController> logger;
		IListRepository<ChannelControllerInfo, int> channelControllers;

		public ChannelControllerController(ILogger<ChannelControllerController> logger, IListRepository<ChannelControllerInfo, int> channelControllers) {

			this.logger = logger;
			this.channelControllers = channelControllers;
		}

		[HttpGet("List")]
		public IEnumerable<ChannelControllerInfo> List() => channelControllers.List();

		[HttpGet("Read")]
		public ChannelControllerInfo Read(int id) => channelControllers.Read(id);

	}
}

