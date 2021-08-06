using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Mvc;

namespace TheLeviathan.ChannelSystem {
	[ApiComponent(ApiModules.Control)]
	public class ChannelsController {

		IChannelService _channels;

		public ChannelsController(IChannelService channels) {
			_channels = channels;
		}

		[HttpGet]
		public void List() {

		}
	}
}