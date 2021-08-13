using Leviathan.Channels.Sdk;
using Leviathan.Services.Sdk;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheLeviathan.ComponentsSystem;

namespace TheLeviathan.ChannelSystem {

	public interface IChannelSystemService {
		IReadOnlyDictionary<int, IChannel> Channels { get; }
	}

	[SingletonService(typeof(IChannelSystemService))]
	public class ChannelSystemService : IChannelSystemService {

		public IReadOnlyDictionary<int, IChannel> Channels { get; }

		public ChannelSystemService(IEnumerable<IChannelProvider> channelProviders) {
			Channels = new ReadOnlyDictionary<int, IChannel>(
				channelProviders.SelectMany(p => p.GetChannels()).ToDictionary(p => p.Id)
			);
		}
	}

	[SingletonService(typeof(IChannelDataService))]
	public class ChannelDataService : IChannelDataService {

		public IEnumerable<ChannelRecord> List() => new[] {
			new ChannelRecord {
				Id=1,
				Name = "Temp1",
				TypeName = ""
			}
		};
	}
}