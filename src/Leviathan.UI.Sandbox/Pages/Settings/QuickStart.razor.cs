using Leviathan.API.Rest.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leviathan.UI.Sandbox.Pages.Settings {
	public partial class QuickStart {
		protected ICollection<QuickStartInfo> Profiles { get; set; }

		protected override async Task OnInitializedAsync() {
			var svcUrl = "https://localhost:44313/";
			using var http = new HttpClient();
			this.Profiles = await new QuickStartServiceClient(svcUrl, http).ProfilesAsync();
		}
	}
}