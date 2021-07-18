using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

		[HttpGet, Route("[action]")]
		public async Task<IEnumerable<ProfileApplication>> Apply(string profileName) =>
			await Service.GetApplication(profileName);

		[HttpPost, Route("[action]")]
		public async Task Apply(string name, IEnumerable<ProfileApplication> applications) =>
			await Service.ApplyProfile(name, applications);
	}
}