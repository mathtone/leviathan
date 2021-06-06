using Leviathan.API.Rest.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leviathan.UI.Sandbox.Pages.Settings {
	public partial class Channels {

		protected ICollection<ChannelTypeInfo> ChannelTypes { get; set; }
		protected ICollection<ChannelInfo> CurrentChannels { get; set; }

		protected override async Task OnInitializedAsync() {
			var svcUrl = "https://localhost:44313/";
			using var http = new HttpClient();

			this.ChannelTypes = await new ChannelTypeServiceClient(svcUrl, http).ListAsync();
			this.CurrentChannels = await new ChannelServiceClient(svcUrl, http).ListAsync();
		}
	}
}
