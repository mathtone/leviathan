using Leviathan.Data;
using System;

namespace Leviathan.Channels.Data {

	public record ChannelRecord : NamedItem<long> {
		public long ComponentId { get; set; }
	}
}