using Leviathan.Channels.SDK;
using System;
using System.Threading.Tasks;
using System.Device.Gpio;
using Microsoft.Extensions.Logging;

namespace Leviathan.GPIO {

	public class GpioChannelData {
		public int Pin { get; init; }
		public int Mode { get; init; }
	}

	public class GpioOnOffChannel : IAsyncInputChannel<bool>, IAsyncOutputChannel<bool> {

		ILogger<GpioOnOffChannel> _log;
		GpioChannelData _data;
		GpioController _controller;

		public GpioOnOffChannel(ILogger<GpioOnOffChannel> log, GpioChannelData data) {
			(_log, _data, _controller) = (log, data, new GpioController());
		}

		public string Id => throw new NotImplementedException();

		public Task<bool> GetValueAsync() {
			throw new NotImplementedException();
		}
		public Task SetValueAsync(bool value) {
			throw new NotImplementedException();
		}

		async Task<object> IAsyncInputChannel.GetValueAsync() => await this.GetValueAsync();
		async Task IAsyncOutputChannel.SetValueAsync(object value) => await this.SetValueAsync((bool)value);

		//public Task SetValue(bool value) {
		//	throw new NotImplementedException();
		//}

		//async Task<object> IAsyncInputChannel.GetValueAsync() => (await this.GetValueAsync());
		//async Task IAsyncOutputChannel.SetValueAsync(object value) => (await this.);

		//public async Task<bool> GetValue() =>
		//	await Task.FromResult(_controller.Read(_data.Pin) == PinValue.High);

		//public Task<bool> GetValueAsync() {
		//	throw new NotImplementedException();
		//}

		//public async Task SetValue(bool value) =>
		//	await Task.Run(() => _controller.Write(_data.Pin, value ? PinValue.High : PinValue.Low));

		//Task<object> IAsyncInputChannel.GetValueAsync() {
		//	throw new NotImplementedException();
		//}
	}
}