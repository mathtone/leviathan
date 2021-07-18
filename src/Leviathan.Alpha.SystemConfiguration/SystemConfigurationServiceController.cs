using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Leviathan.Alpha.SystemConfiguration {
	[ApiComponent, Route("api/[controller]")]
	public class SystemConfigurationServiceController : ServiceController<ISystemConfigurationService> {

		public SystemConfigurationServiceController(ISystemConfigurationService service) :
			base(service) {
		}

		[HttpGet, Route("[action]")]
		public async Task<SystemConfigurationServiceCatalog> Catalog() =>
			await Service.Catalog();

		[HttpPost, Route("[action]")]
		public async Task Apply(string name) =>
			await Service.ApplyProfile(name);
	}
}