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

	public class ChannelInfo {
		public int Id { get; set; }
		public int ModuleId { get; set; }
		public int ChannelTypeId { get; set; }
		public string Name { get; set; }
		//public string ChannelData { get; set; }
		public string ChannelData { get; set; }
		public ChannelInfo() { }
		public ChannelInfo(int moduleId,int channelTypeId,string name, string channelData=default,int id=default) {
			this.Id = id;
			this.ModuleId = moduleId;
			this.ChannelTypeId = channelTypeId;
			this.Name = name;
			this.ChannelData = channelData;
		}
	}

	public class ChannelTypeInfo {
		public int Id { get; set; }
		public string Name { get; set; }
		public string TypeInfo { get; set; }

		public ChannelTypeInfo() { }
		public ChannelTypeInfo(string typeInfo, string name, int id = default) {
			this.Id = id;
			this.TypeInfo = typeInfo;
			this.Name = name;
		}
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