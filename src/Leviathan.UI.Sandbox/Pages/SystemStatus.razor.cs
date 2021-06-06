using Leviathan.API.Rest.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leviathan.UI.Sandbox.Pages {
	public partial class SystemStatus {

		private CoreStatusReport status;

		protected override async Task OnInitializedAsync() {
			using var http = new HttpClient();
			status = await new CoreServiceClient("https://localhost:44313/", http).GetStatusAsync();
		}
	}
}