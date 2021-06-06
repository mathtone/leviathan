using Leviathan.API.Rest.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leviathan.UI.Sandbox.Pages {
	public partial class Drivers {

		protected ICollection<HardwareModuleTypeInfo> ModuleTypes { get; set; }
		protected ICollection<HardwareModuleInfo> Modules { get; set; }
		protected ICollection<ChannelTypeInfo> ChannelTypes { get; set; }
		protected ICollection<ChannelInfo> Channels { get; set; }

		protected override async Task OnInitializedAsync() {
			var svcUrl = "https://localhost:44313/";
			using var http = new HttpClient();
			this.ModuleTypes = await new HardwareModuleTypeServiceClient(svcUrl, http).ListAsync();
			this.Modules = await new HardwareModuleServiceClient(svcUrl, http).ListAsync();
			this.ChannelTypes = await new ChannelTypeServiceClient(svcUrl, http).ListAsync();
			this.Channels = await new ChannelServiceClient(svcUrl, http).ListAsync();
		}
	}
}