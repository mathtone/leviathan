using Leviathan.Channels.Sdk;
using Leviathan.Services.Sdk;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Threading.Tasks;

namespace Leviathan.Drivers.Gpio {

	public interface IGpioService : IChannelProvider {
		GpioController Controller { get; }
	}

	[SingletonService(typeof(IGpioService), typeof(IChannelProvider))]
	public class GpioService : IGpioService, IChannelProvider {

		IChannelDataService _channelData;
		public GpioController Controller { get; }

		public GpioService(IChannelDataService channelData) {
			(_channelData, Controller) = (channelData, new GpioController());
		}

		public IEnumerable<IChannel> GetChannels() {
			if (false)
				yield return null;
		}
	}


	public class GpioChannelData {

		public int Pin { get; init; }
		public PinMode Mode { get; init; }
	}

	public class GpioOnOffChannel : IOChannelBase<bool> {

		IGpioService _gpio;
		GpioChannelData _data;

		public GpioOnOffChannel(IGpioService gpio, GpioChannelData data) {
			(_gpio, _data) = (gpio, data);
			_gpio.Controller.OpenPin(data.Pin, data.Mode);
		}

		public override bool Get() =>
			_gpio.Controller.Read(_data.Pin) != PinValue.Low;

		public override void Set(bool value) =>
			_gpio.Controller.Write(_data.Pin, value ? PinValue.High : PinValue.Low);

		public override void Set(object value) =>
			Set(Convert.ToBoolean(value));
	}
}