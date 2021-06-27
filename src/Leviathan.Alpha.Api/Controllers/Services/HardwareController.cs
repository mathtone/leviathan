using Leviathan.Alpha.Hardware;
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
		public async Task<object> Test() => await Service.Test();
	}
}