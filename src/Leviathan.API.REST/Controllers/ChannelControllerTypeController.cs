using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Leviathan.DataAccess;
using Leviathan.Services.Core.Hardware;

namespace Leviathan.API.REST.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class ChannelControllerTypeController : LeviathanDataController<IChannelControllerTypeData> {

		ILogger<ChannelControllerTypeController> logger;

		public ChannelControllerTypeController(ILogger<ChannelControllerTypeController> logger, IChannelControllerTypeData data) : base(data) {
			this.logger = logger;
		}

		[HttpGet("List")]
		public IEnumerable<ChannelControllerTypeInfo> List() => Data.List();

		[HttpGet("Read")]
		public ChannelControllerTypeInfo Read(int id) => Data.Read(id);
	}
}