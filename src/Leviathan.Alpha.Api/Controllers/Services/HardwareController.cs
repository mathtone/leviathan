using Leviathan.Alpha.Hardware;
using Leviathan.Hardware;
using Leviathan.Hardware.OneWire;
using Leviathan.REST;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UnitsNet;

namespace Leviathan.Alpha.Api.Controllers.Services {
	[ApiController, Route("api/services/[controller]/[action]")]
	public class HardwareController : ServiceControllerBase<ILeviathanHardwareSystemService> {
		public HardwareController(ILeviathanHardwareSystemService service) :
			base(service) {
		}

		[HttpPost]
		public async Task<HardwareSystemCatalog> Test() => await Service.Catalog();
	}

	//[ApiController, Route("api/services/[controller]/{id}")]
	//public class AllChannelsController : ServiceControllerBase<ILeviathanHardwareSystemService> {
	//	public AllChannelsController(ILeviathanHardwareSystemService service) : base(service) {
	//	}

	//	[HttpGet]
	//	public async Task<object> Get(long id) {
	//		await Service.Initialize;
	//		var channel = Service.Channels[id] as IInputChannel<object>;
	//		return channel.GetValue();
	//	}

	//	[HttpPost]
	//	public async Task Set<T>(long id, T value) {
	//		await Service.Initialize;
	//		var channel = Service.Channels[id] as IOutputChannel<bool>;
	//		channel.SetValue(Convert.ToBoolean(value.ToString()));
	//	}
	//}

	[ApiController, Route("api/services/[controller]/{id}")]
	public class OnOffChannels : ServiceControllerBase<ILeviathanHardwareSystemService> {

		public OnOffChannels(ILeviathanHardwareSystemService service) : base(service) {
			;
		}

		[HttpGet]
		public async Task<bool> Get(long id) {
			await Service.Initialize;
			var channel = Service.Channels[id] as IInputChannel<bool>;
			return channel.GetValue();
		}

		[HttpPost]
		public async Task Set(long id, bool value) {
			await Service.Initialize;
			var channel = Service.Channels[id] as IOutputChannel<bool>;
			channel.SetValue(value);
		}
	}

	[ApiController, Route("api/services/[controller]/{id}")]
	public class RatioChannels : ServiceControllerBase<ILeviathanHardwareSystemService> {

		public RatioChannels(ILeviathanHardwareSystemService service) : base(service) {
			;
		}

		[HttpGet]
		public async Task<double> Get(long id) {
			await Service.Initialize;
			var channel = Service.Channels[id] as IInputChannel<double>;
			return channel.GetValue();
		}

		[HttpPost]
		public async Task Set(long id, double value) {
			await Service.Initialize;
			var channel = Service.Channels[id] as IOutputChannel<double>;
			channel.SetValue(value);
		}

	}

	[ApiController, Route("api/services/[controller]/{id}")]
	public class TemperatureChannels : ServiceControllerBase<ILeviathanHardwareSystemService> {
		public TemperatureChannels(ILeviathanHardwareSystemService service) : base(service) {

		}
		[HttpGet]
		public async Task<TempReading> Get(long id) {
			await Service.Initialize;
			var channel = Service.Channels[id] as IInputChannel<Task<TempReading>>;

			return await channel.GetValue();
		}
	}
}