using System.Collections.Generic;

namespace Leviathan.Channels.Sdk {

	public interface IChannelProvider {
		IEnumerable<IChannel> GetChannels();
	}

	public abstract class ChannelProvider : IChannelProvider {
		public abstract IEnumerable<IChannel> GetChannels();
	}

	public interface IChannelDataService {
		IEnumerable<ChannelRecord> List();
	}

	public record ChannelRecord {
		public int Id { get; init; }
		public string Name { get; init; }
		public string TypeName { get; init; }
		public string ChannelData { get; init; }
	}
}