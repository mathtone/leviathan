using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Leviathan.Alpha.SystemConfiguration {
	[ApiComponent]
	public class SystemConfigurationServiceController : ServiceController<ISystemConfigurationService> {

		public SystemConfigurationServiceController(ISystemConfigurationService service) :
			base(service) {
		}

		[HttpGet, Route("[action]")]
		public async Task<SystemConfigurationServiceCatalog> Catalog() =>
			await Service.Catalog();
	}
}