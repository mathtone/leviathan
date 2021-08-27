using Leviathan.Data;
using Leviathan.Services;
using System;

namespace TheLeviathan.ChannelSystem {
	public interface IChannelService {

	}

	

	[SingletonService(typeof(IChannelService))]
	public class ChannelService : IChannelService {

	}
}