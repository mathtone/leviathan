using Leviathan.Channels.Sdk;
using Leviathan.Services.Sdk;
using System;
using System.Collections.Generic;

namespace Leviathan.Drivers.Pca9685 {
	[SingletonService(typeof(IChannelProvider))]
	public class Pca9695ChannelProvider : IChannelProvider {
		public IEnumerable<IChannel> GetChannels() {
			return Array.Empty<IChannel>();
		}
	}
}
