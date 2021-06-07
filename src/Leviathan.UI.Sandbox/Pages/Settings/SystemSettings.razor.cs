using Leviathan.API.Rest.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leviathan.UI.Sandbox.Pages.Settings {
	public partial class SystemSettings {

		DestructCode destructCode;

		async void GetDestructCode() {
			var svcUrl = "https://localhost:44313/";
			using var http = new HttpClient();
			this.destructCode = await new CoreServiceClient(svcUrl, http).DestructCodeAsync();
			await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
		}


		async void SendDestructCode() {
			var svcUrl = "https://localhost:44313/";
			using var http = new HttpClient();
			await new CoreServiceClient(svcUrl, http).FactoryResetAsync(destructCode.Code);
			this.destructCode = null;
			await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
		}
	}
}