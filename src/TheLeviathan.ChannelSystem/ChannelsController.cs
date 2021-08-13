using Leviathan.Channels.Sdk;
using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLeviathan.ChannelSystem {

	[ApiComponent(ApiModules.Control)]
	public class ChannelsController {

		IChannelSystemService _channels;

		public ChannelsController(IChannelSystemService channels) {
			_channels = channels;
		}

		[HttpGet, Route("[action]")]
		public IEnumerable<ChannelListing> List() => _channels.Channels.Values.Select(
			c => new ChannelListing {
				Id = c.Id,
				Name = c.Name,
				TypeName = c.GetType().Name
			}
		);

		[HttpGet]
		public async Task<object> ReadAsync(int id) {

			if (_channels.Channels[id] is IAsyncInputChannel ach)
				return await ach.GetAsync();
			else if (_channels.Channels[id] is IInputChannel ch)
				return ch.Get();

			throw new Exception($"Could not read from channel {id}");
		}

		[HttpPost]
		public async Task WriteAsync(int id, object value) {

			if (_channels.Channels[id] is IAsyncOutputChannel ach)
				await ach.SetAsync(value);

			else if (_channels.Channels[id] is IOutputChannel ch)
				ch.Set(value);

			throw new Exception($"Could not write to channel {id}");
		}
	}

	public class ChannelListing {
		public int Id { get; init; }
		public string Name { get; init; }
		public string TypeName { get; init; }
	}
}