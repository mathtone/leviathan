using Leviathan.Alpha.Hardware;
using Leviathan.Hardware;
using Leviathan.REST;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Api.Controllers.Services {
	[ApiController, Route("api/services/[controller]/[action]")]
	public class HardwareController : ServiceControllerBase<ILeviathanHardwareSystemService> {
		public HardwareController(ILeviathanHardwareSystemService service) :
			base(service) {
		}

		[HttpPost]
		public async Task<HardwareSystemCatalog> Test() => await Service.Catalog();
	}


	[ApiController, Route("api/services/[controller]/{id}")]
	public class ChannelsController : ServiceControllerBase<ILeviathanHardwareSystemService> {
		public ChannelsController(ILeviathanHardwareSystemService service) :
			base(service) {
		}

		[HttpGet]
		public async Task<object> Get(long id) {
			await Service.Initialize;
			var channel = Service.Channels[id] as IInputChannel<object>;
			return channel.GetValue();
		}


		[HttpPut]
		public async Task Set(long id, bool value) {
			await Service.Initialize;
			var channel = Service.Channels[id] as IOutputChannel<bool>;
			channel.SetValue(value);
		}
	}
}