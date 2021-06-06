using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Leviathan.DataAccess;
using Leviathan.Services.Core.Hardware;

namespace Leviathan.API.REST.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class ChannelController : LeviathanDataController<IChannelData> {

		ILogger<ChannelController> logger;

		public ChannelController(ILogger<ChannelController> logger, IChannelData data) : base(data) {
			this.logger = logger;
		}

		[HttpGet("List")]
		public IEnumerable<ChannelInfo> List() => Data.List();

		[HttpGet("Read")]
		public ChannelInfo Read(int id) => Data.Read(id);

	}
}