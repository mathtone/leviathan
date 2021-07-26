namespace Leviathan.OneWire {
	public class TempSensorChannelData {

		public string BusId { get; init; }
		public string DeviceId { get; init; }

		public TempSensorChannelData() { }
		public TempSensorChannelData(string busId, string deviceId) {
			this.BusId = busId;
			this.DeviceId = deviceId;
		}
	}
}