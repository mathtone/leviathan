using Leviathan.API.Rest.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leviathan.UI.Sandbox.Pages.Settings {
	public partial class Modules {
		protected ICollection<HardwareModuleTypeInfo> ModuleTypes { get; set; }
		protected ICollection<HardwareModuleInfo> CurrentModules { get; set; }

		protected override async Task OnInitializedAsync() {
			var svcUrl = "https://localhost:44313/";
			using var http = new HttpClient();
			this.ModuleTypes = await new HardwareModuleTypeServiceClient(svcUrl, http).ListAsync();
			this.CurrentModules = await new HardwareModuleServiceClient(svcUrl, http).ListAsync();
		}
	}
}
