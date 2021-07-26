using System.Collections.Generic;

namespace Leviathan.Channels.SDK {
	public interface IChannel {
		string Id { get; }
	}

	public interface IChannelProvider {
		IEnumerable<IChannel> Channels { get; }
	}
}
