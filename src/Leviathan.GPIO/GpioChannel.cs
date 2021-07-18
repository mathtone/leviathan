using Leviathan.Channels.SDK;
using System;
using System.Threading.Tasks;

namespace Leviathan.GPIO {
	public class GpioChannelData {
		public int Pin { get; init; }
		public int Mode { get; init; }
	}
	public class GpioChannel : IAsyncInputChannel<bool>, IAsyncOutputChannel<bool> {
		public GpioChannel(GpioChannelData data) {
			;
		}
		public Task<bool> GetValue() {
			throw new NotImplementedException();
		}

		public Task SetValue(bool value) {
			throw new NotImplementedException();
		}
	}
}