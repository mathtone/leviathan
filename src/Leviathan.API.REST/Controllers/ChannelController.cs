using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Leviathan.DataAccess;

namespace Leviathan.API.REST.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class ChannelController : ControllerBase {

		ILogger<ChannelController> logger;
		IListRepository<ChannelInfo, int> channels;

		public ChannelController(ILogger<ChannelController> logger, IListRepository<ChannelInfo, int> channels) {

			this.logger = logger;
			this.channels = channels;
		}

		[HttpGet("List")]
		public IEnumerable<ChannelInfo> List() => channels.List();

		[HttpGet("Read")]
		public ChannelInfo Read(int id) => channels.Read(id);

	}
}