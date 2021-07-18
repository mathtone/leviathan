using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc;

namespace Leviathan.Alpha.Channels {
	[ApiComponent, Route("api/[controller]")]
	public class ChannelsServiceController : ServiceController<IChannelsService> {
		public ChannelsServiceController(IChannelsService service) : base(service) {
		}

		[HttpGet]
		public void Test() { }
	}
}