using Leviathan.Rest.Api.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leviathan.UI.Beta.Pages.Setup {
	public partial class Configuration : IDisposable {
		
		ICollection<ConfigurationProfileListing> Profiles { get; set; }
		HttpClient http = new HttpClient();
		string svcUrl = "https://localhost:44377/";

		protected override async Task OnInitializedAsync() {
			await base.OnInitializedAsync();
			Profiles = await new InitializationClient(svcUrl, http).ListProfilesAsync(); ;
		}

		async Task ApplyProfile(string name) {
			var client = new InitializationClient(svcUrl, http);
			await client.FactoryResetAsync();
			await client.ApplyProfileAsync(name);
		}

		//async Task FactoryReset() {
		//	await new InitializationClient(svcUrl, http).FactoryResetAsync();
		//}

		public void Dispose() {
			http.Dispose();
		}
	}
}