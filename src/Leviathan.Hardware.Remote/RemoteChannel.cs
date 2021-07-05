using Leviathan.Components;
using System;
[assembly: LeviathanPlugin("Remote Channels")]
namespace Leviathan.Hardware.Remote {
	//public class RemoteConnector {

	//}
	public class RemoteChannelData {
		public long PartnerId { get; init; }
	}

	public class RemoteChannel<T> : IInputChannel<T>,IOutputChannel<T> {
		RemoteChannelData _channelData;
		public RemoteChannel(RemoteChannelData data) {
			;
		}
		public T GetValue() {
			throw new NotImplementedException();
		}

		public void SetValue(T value) {
			throw new NotImplementedException();
		}
	}
}