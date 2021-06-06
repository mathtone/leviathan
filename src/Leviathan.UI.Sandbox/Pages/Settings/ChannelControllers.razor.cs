using Leviathan.API.Rest.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leviathan.UI.Sandbox.Pages.Settings {
	public partial class ChannelControllers {
		protected ICollection<ChannelControllerTypeInfo> ChannelControllerTypes { get; set; }
		protected ICollection<ChannelControllerInfo> CurrentChannelControllers { get; set; }

		protected override async Task OnInitializedAsync() {
			var svcUrl = "https://localhost:44313/";
			using var http = new HttpClient();

			this.ChannelControllerTypes = await new ChannelControllerTypeServiceClient(svcUrl, http).ListAsync();
			this.CurrentChannelControllers = await new ChannelControllerServiceClient(svcUrl, http).ListAsync();
		}
	}
}
