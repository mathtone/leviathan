using Leviathan.Rest.Api.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leviathan.UI.Alpha.Pages.Admin {
	public partial class Setup : IDisposable {
		readonly string svcUrl = "https://localhost:44377/";
		readonly HttpClient client = new HttpClient();
		SystemConfiguration CurrentConfig { get; set; }
		IEnumerable<ConfigurationProfileListing> Profiles { get; set; } = new ConfigurationProfileListing[0];

		protected override async Task OnInitializedAsync() {
			await base.OnInitializedAsync();
			CurrentConfig = await new InitializationClient(svcUrl, client).CurrentConfigAsync();
			Profiles = await new InitializationClient(svcUrl, client).ListProfilesAsync();
		}

		protected async void ApplyConfig() =>
			await new InitializationClient(svcUrl, client).ConfigureAsync(CurrentConfig);
		

		protected async void FactoryReset() =>
			await new InitializationClient(svcUrl, client).FactoryResetAsync();
		

		protected async void ApplyProfile(string name) =>			
			await new InitializationClient(svcUrl, client).ApplyProfileAsync(name);

		public void Dispose() {
			client.Dispose();
		}
	}
}