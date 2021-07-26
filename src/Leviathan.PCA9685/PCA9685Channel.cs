using Iot.Device.Pwm;
using Leviathan.Channels.SDK;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Leviathan.PCA9685 {
	public class Pca9685ChannelData {
		public int PwmChannelId { get; init; }
	}

	public class Pca9685Channel : IAsyncInputChannel<double>, IAsyncOutputChannel<double> {

		Pca9685 _device;
		Pca9685ChannelData _data;
		ILogger<Pca9685Channel> _log;

		public Pca9685Channel(ILogger<Pca9685Channel> log, Pca9685 device, Pca9685ChannelData data) =>
			(_log, _device, _data) = (log, device, data);

		public string Id => throw new NotImplementedException();

		public async Task<double> GetValueAsync() =>
			await Task.FromResult(_device.GetDutyCycle(_data.PwmChannelId));

		public async Task SetValueAsync(double value) => await Task.Run(() => _device.SetDutyCycle(_data.PwmChannelId, value));

		async Task IAsyncOutputChannel.SetValueAsync(object value) => await this.SetValueAsync((double)value);

		async Task<object> IAsyncInputChannel.GetValueAsync() => await this.GetValueAsync();
	}
}