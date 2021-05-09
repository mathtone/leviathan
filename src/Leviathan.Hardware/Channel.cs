using System;

namespace Leviathan.Hardware {
	public interface IChannel {
		int Id { get; }
		string Name { get; }
	}

	public class ChannelConfig {
		public int ChannelId { get; set; }
		public int ModuleId { get; set; }
		public string Name { get; set; }
		public Type ChannelType { get; set; }
	}

	public class Channel<DEVICE, CONFIG> : IChannel, IDisposable where CONFIG : ChannelConfig {
		private bool disposedValue;

		protected DEVICE Device { get; }
		protected CONFIG Config { get; }
		public int Id => Config.ChannelId;
		public string Name => Config.Name;
		public Channel(DEVICE device, CONFIG config) {
			this.Device = device;
			this.Config = config;
		}

		protected virtual void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) {
					OnDispose();
				}
				disposedValue = true;
			}
		}

		public void Dispose() {
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void OnDispose() { }
	}
}