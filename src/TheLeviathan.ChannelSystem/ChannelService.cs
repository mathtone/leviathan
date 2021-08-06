using Leviathan.Services.Sdk;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TheLeviathan.ComponentsSystem;

namespace TheLeviathan.ChannelSystem {

	public interface IChannelService  {
	
	}

	[SingletonService(typeof(IChannelService))]
	public class ChannelService : IChannelService {

		IComponentsService _components;

		public ChannelService(IComponentsService components) {
			_components = components;
		}
	}
}
