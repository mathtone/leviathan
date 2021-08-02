namespace TheLeviathan.OneWire {
	public class OneWireChannelData {
		public string BusId { get; set; }
		public string DeviceId { get; set; }
	}

	public class OneWireChannel {

		IOneWireService _service;
		OneWireChannelData _channelData;

		public OneWireChannel(IOneWireService service, OneWireChannelData channelData) {
			(_service, _channelData) = (service, channelData);
		}
	}
}