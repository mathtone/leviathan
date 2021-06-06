using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Leviathan.DataAccess;
using Leviathan.Services.Core.Hardware;

namespace Leviathan.API.REST.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class ChannelTypeController : LeviathanDataController<IChannelTypeData> {

		ILogger<ChannelTypeController> logger;
		IListRepository<ChannelTypeInfo, int> channelTypes;

		public ChannelTypeController(ILogger<ChannelTypeController> logger, IChannelTypeData data) : base(data) {

			this.logger = logger;
		}

		[HttpGet("List")]
		public IEnumerable<ChannelTypeInfo> List() => Data.List();

		[HttpGet("Read")]
		public ChannelTypeInfo Read(int id) => Data.Read(id);
	}
}