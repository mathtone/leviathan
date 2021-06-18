using Leviathan.Alpha.Api.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leviathan.Alpha.UI.Main.Pages.Admin {
	public partial class Startup {
		DestructCode DestructCode { get; set; }
		StartupServiceCatalog Catalog { get; set; }

		string DestructCodeValue { get; set; }
		string ConfirmCode { get; set; }

		protected override async Task OnInitializedAsync() {
			await base.OnInitializedAsync();
			using var http = new HttpClient();
			Catalog = await new StartupServiceClient("https://localhost:44368/", http).CatalogAsync();
		}

		protected async void Save() {
			using var http = new HttpClient();
			await new StartupServiceClient("https://localhost:44368/", http).SetStartupInfoAsync(Catalog.CurrentConfig.StartupInfo);
			StateHasChanged();
		}

		protected async void Apply() {
			using var http = new HttpClient();
			await new StartupServiceClient("https://localhost:44368/", http).ApplyConfigurationAsync();
			StateHasChanged();
		}

		protected async void BeginSelfSestruct() {
			using var http = new HttpClient();
			DestructCode = await new FactoryResetServiceClient("https://localhost:44368/", http).BeginSelfDestructAsync();
			DestructCodeValue = DestructCode.Code;
			StateHasChanged();
		}
		protected async void FactoryReset() {
			using var http = new HttpClient();
			await new FactoryResetServiceClient("https://localhost:44368/", http).FactoryResetAsync(ConfirmCode);
			DestructCode = null;
			StateHasChanged();
		}		
	}
}