using Leviathan.Channels.SDK;
using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Channels {
	[ApiComponent, Route("api/[controller]")]
	public class ChannelSystemController : ServiceController<IChannelSystemService> {
		public ChannelSystemController(IChannelSystemService service) : base(service) {
			;
		}

		[HttpGet]
		public Task<object> Test(string id) => Service.GetValueAsync(id);
	}
}